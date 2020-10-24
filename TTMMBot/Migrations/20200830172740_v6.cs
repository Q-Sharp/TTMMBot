﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTMMBot.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guildSettings",
                columns: table => new
                {
                    guildSettingsId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(nullable: true),
                    WaitForReaction = table.Column<TimeSpan>(nullable: false),
                    UseTriggers = table.Column<bool>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ClanSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guildSettings", x => x.guildSettingsId);
                });

            migrationBuilder.CreateTable(
                name: "Restart",
                columns: table => new
                {
                    RestartId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guild = table.Column<ulong>(nullable: false),
                    Channel = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restart", x => x.RestartId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guildSettings");

            migrationBuilder.DropTable(
                name: "Restart");
        }
    }
}
