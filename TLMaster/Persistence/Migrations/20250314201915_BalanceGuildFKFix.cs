using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BalanceGuildFKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Guilds_CharacterId",
                table: "Balance");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_GuildId",
                table: "Balance",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Guilds_GuildId",
                table: "Balance",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Guilds_GuildId",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_Balance_GuildId",
                table: "Balance");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Guilds_CharacterId",
                table: "Balance",
                column: "CharacterId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
