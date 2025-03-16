using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TLMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuctionInitialPriceIntoBidStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialPrice",
                table: "Auctions",
                newName: "BidStep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BidStep",
                table: "Auctions",
                newName: "InitialPrice");
        }
    }
}
