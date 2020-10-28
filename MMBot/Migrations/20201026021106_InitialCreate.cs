﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuildId = table.Column<ulong>(nullable: false),
                    TextChannelId = table.Column<ulong>(nullable: false),
                    AnswerTextChannelId = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SortOrder = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DiscordRole = table.Column<string>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuildId = table.Column<ulong>(nullable: false),
                    Prefix = table.Column<string>(nullable: true),
                    WaitForReaction = table.Column<TimeSpan>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ClanSize = table.Column<int>(nullable: false),
                    MemberMovementQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guild = table.Column<ulong>(nullable: false),
                    Channel = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Discord = table.Column<string>(nullable: true),
                    AHigh = table.Column<int>(nullable: true),
                    SHigh = table.Column<int>(nullable: true),
                    Donations = table.Column<int>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    DiscordStatus = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ClanId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    Join = table.Column<int>(nullable: false),
                    IgnoreOnMoveUp = table.Column<bool>(nullable: false),
                    PlayerTag = table.Column<string>(nullable: true),
                    AutoSignUpForFightNight = table.Column<bool>(nullable: false),
                    GuildId = table.Column<ulong>(nullable: false),
                    MemberGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Clan_ClanId",
                        column: x => x.ClanId,
                        principalTable: "Clan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Member_MemberGroup_MemberGroupId",
                        column: x => x.MemberGroupId,
                        principalTable: "MemberGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacation_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clan_SortOrder",
                table: "Clan",
                column: "SortOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clan_Tag",
                table: "Clan",
                column: "Tag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_ClanId",
                table: "Member",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MemberGroupId",
                table: "Member",
                column: "MemberGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Name",
                table: "Member",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_MemberId",
                table: "Vacation",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "GuildSettings");

            migrationBuilder.DropTable(
                name: "Restart");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Clan");

            migrationBuilder.DropTable(
                name: "MemberGroup");
        }
    }
}