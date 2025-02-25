using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ItemGuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuildId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Items_GuildId",
                table: "Items",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Guilds_GuildId",
                table: "Items",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Guilds_GuildId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_GuildId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Items");
        }
    }
}
