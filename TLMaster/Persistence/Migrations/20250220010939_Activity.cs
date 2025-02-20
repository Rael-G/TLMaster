using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Activity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterParty_Parties_PartyId",
                table: "CharacterParty");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Guilds_GuildId",
                table: "Parties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parties",
                table: "Parties");

            migrationBuilder.RenameTable(
                name: "Parties",
                newName: "Party");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Bids",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_GuildId",
                table: "Party",
                newName: "IX_Party_GuildId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Guilds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Guilds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Party",
                table: "Party",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payout = table.Column<int>(type: "int", nullable: false),
                    WasPaid = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPasswordRequired = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityCharacter",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCharacter", x => new { x.ActivityId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ActivityCharacter_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityCharacter_Characters_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_GuildId",
                table: "Activity",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCharacter_ParticipantsId",
                table: "ActivityCharacter",
                column: "ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterParty_Party_PartyId",
                table: "CharacterParty",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Guilds_GuildId",
                table: "Party",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterParty_Party_PartyId",
                table: "CharacterParty");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Guilds_GuildId",
                table: "Party");

            migrationBuilder.DropTable(
                name: "ActivityCharacter");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Party",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Guilds");

            migrationBuilder.RenameTable(
                name: "Party",
                newName: "Parties");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Bids",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Party_GuildId",
                table: "Parties",
                newName: "IX_Parties_GuildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parties",
                table: "Parties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterParty_Parties_PartyId",
                table: "CharacterParty",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Guilds_GuildId",
                table: "Parties",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
