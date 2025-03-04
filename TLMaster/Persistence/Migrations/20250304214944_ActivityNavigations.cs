using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActivityNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityCharacter_Activity_ActivityId",
                table: "ActivityCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterParty_Party_PartyId",
                table: "CharacterParty");

            migrationBuilder.RenameColumn(
                name: "PartyId",
                table: "CharacterParty",
                newName: "PartiesId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterParty_PartyId",
                table: "CharacterParty",
                newName: "IX_CharacterParty_PartiesId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "ActivityCharacter",
                newName: "ActivitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityCharacter_Activity_ActivitiesId",
                table: "ActivityCharacter",
                column: "ActivitiesId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterParty_Party_PartiesId",
                table: "CharacterParty",
                column: "PartiesId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityCharacter_Activity_ActivitiesId",
                table: "ActivityCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterParty_Party_PartiesId",
                table: "CharacterParty");

            migrationBuilder.RenameColumn(
                name: "PartiesId",
                table: "CharacterParty",
                newName: "PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterParty_PartiesId",
                table: "CharacterParty",
                newName: "IX_CharacterParty_PartyId");

            migrationBuilder.RenameColumn(
                name: "ActivitiesId",
                table: "ActivityCharacter",
                newName: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityCharacter_Activity_ActivityId",
                table: "ActivityCharacter",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterParty_Party_PartyId",
                table: "CharacterParty",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
