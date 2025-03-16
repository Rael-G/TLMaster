using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CharacterPartyOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterParty");

            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                table: "Characters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PartyId",
                table: "Characters",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Party_PartyId",
                table: "Characters",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Party_PartyId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PartyId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "CharacterParty",
                columns: table => new
                {
                    CharactersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterParty", x => new { x.CharactersId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_CharacterParty_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterParty_Party_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterParty_PartiesId",
                table: "CharacterParty",
                column: "PartiesId");
        }
    }
}
