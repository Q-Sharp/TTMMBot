﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace TTMMBot.Services.Interfaces
{
    public interface ICommandHandler
    {
        Task InitializeAsync();
        Task HandleCommandAsync(SocketMessage arg);
        Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result);
        Task AddToReactionList(IUserMessage message, Func<IEmote, IUser, Task> fT);
        Task AddChannelToGoogleFormsWatchList(IGuildChannel channel);
        Task RemoveChannelFromGoogleFormsWatchList(IGuildChannel channel);
    }
}