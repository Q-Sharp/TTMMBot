﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTMMBot.Data;

namespace TTMMBot.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("TTMMBot.Data.Entities.Clan", b =>
                {
                    b.Property<int>("ClanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiscordRole")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClanID");

                    b.HasIndex("Tag")
                        .IsUnique();

                    b.ToTable("Clan");
                });

            modelBuilder.Entity("TTMMBot.Data.Entities.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AHigh")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClanID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discord")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Donations")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Join")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SHigh")
                        .HasColumnType("INTEGER");

                    b.HasKey("MemberID");

                    b.HasIndex("ClanID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Member");
                });

            modelBuilder.Entity("TTMMBot.Data.Entities.Vacation", b =>
                {
                    b.Property<int>("VacationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("VacationID");

                    b.HasIndex("MemberID");

                    b.ToTable("Vacation");
                });

            modelBuilder.Entity("TTMMBot.Data.Entities.Member", b =>
                {
                    b.HasOne("TTMMBot.Data.Entities.Clan", "Clan")
                        .WithMany("Member")
                        .HasForeignKey("ClanID");
                });

            modelBuilder.Entity("TTMMBot.Data.Entities.Vacation", b =>
                {
                    b.HasOne("TTMMBot.Data.Entities.Member", "Member")
                        .WithMany("Vacation")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
