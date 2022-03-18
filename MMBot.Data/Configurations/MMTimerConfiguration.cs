﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMBot.Data.Contracts.Entities;

namespace MMBot.Data.Configurations;

public class MMTimerConfiguration : IEntityTypeConfiguration<MMTimer>
{
    public void Configure(EntityTypeBuilder<MMTimer> builder)
    {
        builder.UseXminAsConcurrencyToken()
               .HasKey(c => c.Id);
    }
}
