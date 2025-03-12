using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CharacterGuildApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterGuild",
                columns: table => new
                {
                    ApplicantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGuild", x => new { x.ApplicantsId, x.ApplicationsId });
                    table.ForeignKey(
                        name: "FK_CharacterGuild_Characters_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGuild_Guilds_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGuild_ApplicationsId",
                table: "CharacterGuild",
                column: "ApplicationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterGuild");
        }
    }
}
