using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class BidDto
{
    public Guid Id { get; set; }
    public CharacterSummaryDto Bidder { get; set; } = new();
    public AuctionSummaryDto Auction { get; set; } = new();
    public int Amount { get; set; }
}
