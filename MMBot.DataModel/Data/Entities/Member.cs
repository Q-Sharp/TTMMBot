﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MMBot.Data.Enums;
using MMBot.Data.Interfaces;
using System.Collections.Generic;

namespace MMBot.Data.Entities
{
    public class Member : IHaveId, IHaveIdentifier
    {
        public int Id { get; set; }

        [Required]
        [Display]
        public string Name { get; set; }

        [JsonIgnore]
        public string Identitfier => Name;

        [Display]
        [IgnoreDataMember]
        [JsonIgnore]
        public string ClanTag => Clan?.Tag;

        [Display]
        public string Discord { get; set; }

        [Display]
        public int? AHigh { get; set; }

        [Display]
        public int? SHigh { get; set; }

        public int? Current { get; set; }

        [Display]
        public int? Donations { get; set; }

        [Display]
        public Role Role { get; set; }

        public DiscordStatus DiscordStatus { get; set; } = DiscordStatus.Active;

        [Display]
        public bool IsActive { get; set; }

        public int? ClanId { get; set; }

        [JsonIgnore]
        public virtual Clan Clan { get; set; }

        [JsonIgnore]
        public virtual IList<Vacation> Vacation { get; set; } = new List<Vacation>();

        public DateTime? LastUpdated { get; set; }

        public int Join { get; set; }

        [Display]
        public bool IgnoreOnMoveUp { get; set; }

        [Display]
        public string PlayerTag { get; set; }

        public bool AutoSignUpForFightNight { get; set; }

        public ulong GuildId { get; set; }

        public int? MemberGroupId { get; set; }

        [JsonIgnore]
        public virtual MemberGroup MemberGroup { get; set; }

        [JsonIgnore]
        [Display]
        public int? SHighLowest => MemberGroup?.Members?.Min(x => x.SHigh);

        [Display]
        public double? LocalTimeOffSet { get; set; }

        [Display]
        [IgnoreDataMember]
        [JsonIgnore]
        public DateTime? LocalDateTime 
            => LocalTimeOffSet.HasValue ? DateTime.UtcNow + TimeSpan.FromHours(LocalTimeOffSet.Value) : null;

        [JsonIgnore]
        public virtual IList<Strike> Strikes { get; set; } = new List<Strike>();

        public override string ToString() => Clan?.Tag is not null ? $"[{Clan?.Tag}] {Name}" : $"{Name}";

        public void Update(object member)
        {
            if(member is Member m && (Id == m.Id || Name == m.Name))
            {
                Name = m.Name;
                Discord = m.Discord;
                AHigh = m.AHigh;
                SHigh = m.SHigh;
                Donations = m.Donations;
                Role = m.Role;
                DiscordStatus = m.DiscordStatus;
                IsActive = m.IsActive;
                ClanId = m.ClanId;
                LastUpdated = m.LastUpdated;
                Join = m.Join;
                IgnoreOnMoveUp = m.IgnoreOnMoveUp;
                PlayerTag = m.PlayerTag;
                AutoSignUpForFightNight = m.AutoSignUpForFightNight;
                GuildId = m.GuildId;
                MemberGroupId = m.MemberGroupId;
                LocalTimeOffSet = m.LocalTimeOffSet;
            }
        }
    }
}