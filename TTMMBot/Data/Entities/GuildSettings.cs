﻿using System;
using System.ComponentModel.DataAnnotations;
using TTMMBot.Helpers;

namespace TTMMBot.Data.Entities
{
    public class GuildSettings : IHaveId
    {
        [Key]
        public int Id { get; set; }

        [Display]
        public ulong GuildId { get; set; }

        // Discord
        [Display]
        public string Prefix { get; set; }

        [Display]
        public TimeSpan WaitForReaction { get; set; }

        // Filesystem
        [Display]
        public string FileName { get; set; }

        // InGame
        [Display]
        public int ClanSize { get; set; }

        [Display]
        public int MemberMovementQty { get; set; }

        public void Update(object guildSettings)
        {
            if(guildSettings is GuildSettings gs && Id == gs.Id)
                this.ChangeProperties(gs);
        }
    }
}