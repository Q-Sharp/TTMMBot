﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MMBot.Data.Entities;

namespace MMBot.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<Clan> CreateClanAsync(ulong guildId);
        Task<IList<Clan>> LoadClansAsync(ulong? guildId = null);
        Task<Clan> GetClanAsync(string tag, ulong? guildId = null);
        void DeleteClan(Clan c);

        Task<Member> CreateMemberAsync(ulong guildId);
        Task<IList<Member>> LoadMembersAsync(ulong? guildId = null);
        void DeleteMember(Member m);

        Task<GuildSettings> LoadGuildSettingsAsync(ulong guildId);

        Task<Restart> AddRestart();
        Task<Tuple<ulong, ulong>> ConsumeRestart();

        Task<Channel> CreateChannelAsync(ulong guildId);
        Task<IList<Channel>> LoadChannelsAsync(ulong? guildId = null);
        void DeleteChannel(Channel c);

        Task<MMTimer> CreateTimerAsync(ulong guildId);
        Task<IList<MMTimer>> LoadTimerAsync(ulong? guildId = null);
        Task<MMTimer> GetTimerAsync(string name, ulong? guildId = null);
        void DeleteTimer(MMTimer c);

        Task SaveDataAsync();
        Task MigrateAsync();
        Task CleanDB(IEnumerable<ulong> guildIds = null);
    }
}
