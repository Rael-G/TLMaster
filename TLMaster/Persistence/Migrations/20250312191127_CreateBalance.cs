using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coin",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuildId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balance_Guilds_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balance_CharacterId",
                table: "Balance",
                column: "CharacterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.AddColumn<int>(
                name: "Coin",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
