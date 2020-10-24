﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TTMMBot.Helpers;
using TTMMBot.Modules.Interfaces;
using TTMMBot.Services.Interfaces;

namespace TTMMBot.Services.CommandHandler
{
    public partial class CommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly ILogger<CommandHandler> _logger;
        private readonly IList<ISocketMessageChannel> _channelList = new List<ISocketMessageChannel>();

        private IGuildSettingsService _gs;
        private IDatabaseService _databaseService;
        private IGoogleFormsService _googleFormsSubmissionService;

        public CommandHandler(IServiceProvider services, CommandService commands, DiscordSocketClient client, ILogger<CommandHandler> logger)
        {
            _commands = commands;
            _services = services;
            _client = client;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(GetType().Assembly, _services);

            _databaseService = _services.GetService<IDatabaseService>();
            _gs = _services.GetService<IGuildSettingsService>();
            _googleFormsSubmissionService = _services.GetService<IGoogleFormsService>();

            _commands.CommandExecuted += CommandExecutedAsync;

            _client.MessageReceived += Client_HandleCommandAsync;
            _client.ReactionAdded += Client_ReactionAdded;
            _client.Disconnected += Client_Disconnected;

            _client.Ready += Client_Ready;
        }

        private async Task Client_Ready()
        {
            var r = await _databaseService.ConsumeRestart();
            _logger.Log(LogLevel.Information, "Bot is connected!");

            if (r != null)
                await _client.GetGuild(r.Item1)
                    .GetTextChannel(r.Item2)
                    .SendMessageAsync("Bot service has been restarted!");

            await _databaseService.CleanDB();

            (await _databaseService.LoadChannelsAsync())?
                .Select(x => _client.GetGuild(x.GuildId).GetTextChannel(x.TextChannelId))?
                .ForEach(x => _channelList.Add(x));
        }

        private async Task Client_Disconnected(Exception arg)
        {
            _logger.LogError(arg.Message);

            await Task.Run(() =>
            { 
                Process.Start(AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            });
        }

        public async Task Client_HandleCommandAsync(SocketMessage arg)
        {
            if (_channelList.Contains(arg.Channel))
            {
                var urls = arg.Content.GetUrl();
                urls.ForEach(async x =>
                {
                    var m = await _databaseService.LoadMembersAsync();

                    m.Where(z => z.AutoSignUpForFightNight && z.PlayerTag != null).ForEach(async y => await _googleFormsSubmissionService.SubmitAsync(x, "@Tag"));
                });
            }

            if (!(arg is SocketUserMessage msg) || msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;
            _gs?.LoadSettings((msg.Channel as IGuildChannel).Id);

            var pos = 0;
            if (msg.HasStringPrefix(_gs.Prefix, ref pos) || msg.HasMentionPrefix(_client.CurrentUser, ref pos) || msg.Content.ToLower().StartsWith(_gs.Prefix.ToLower()))
            {
                var context = new SocketCommandContext(_client, msg);
                var result = await _commands.ExecuteAsync(context, pos, _services);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (result.IsSuccess)
                return;

            if(!command.IsSpecified)
            {
                await context.Channel.SendMessageAsync($"I don't know this command: {context.Message}");
                return;
            }
                
            await context.Channel.SendMessageAsync($"error: {result}");
        }

        public Task AddChannelToGoogleFormsWatchList(IGuildChannel channel)
        {
            lock (_messageIdWithReaction)
            {
                _channelList.Add(channel as ISocketMessageChannel);
            }

            return Task.Run(async () => 
            {
                var c = await _databaseService.CreateChannelAsync();
                c.TextChannelId = channel.Id;
                c.GuildId = channel.GuildId;
                await _databaseService.SaveDataAsync();
            });
        }

        public Task RemoveChannelFromGoogleFormsWatchList(IGuildChannel channel)
        {
            lock (_messageIdWithReaction)
            {
                _channelList.Remove(channel as ISocketMessageChannel);
            }

            return Task.Run(async () => 
            {
                var c = (await _databaseService.LoadChannelsAsync()).FirstOrDefault(x => x.GuildId == channel.GuildId && x.TextChannelId == channel.Id);

                if(c != null)
                    _databaseService.DeleteChannel(c);

                await _databaseService.SaveDataAsync();
            });
        }
    }
}
