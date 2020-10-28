﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MMBot.Services.Interfaces
{
    public interface ICommandHandler : IMMBotInterface 
    {
        Task InitializeAsync();
        Task Client_HandleCommandAsync(SocketMessage arg);
        Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result);
        Task AddToReactionList(IUserMessage message, Func<IEmote, IUser, Task> fT, bool allowMultiple = true);
        void AddChannelToGoogleFormsWatchList(IGuildChannel channel, IGuildChannel qChannel);
        void RemoveChannelFromGoogleFormsWatchList(IGuildChannel channel);
    }
}