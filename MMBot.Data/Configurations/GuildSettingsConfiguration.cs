﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMBot.Data.Contracts.Entities;

namespace MMBot.Data.Configurations;

public class GuildSettingsConfiguration : IEntityTypeConfiguration<GuildSettings>
{
    public void Configure(EntityTypeBuilder<GuildSettings> builder)
    {
        builder.UseXminAsConcurrencyToken()
               .HasKey(c => c.Id);
    }
}
