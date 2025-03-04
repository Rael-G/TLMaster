using TLMaster.UI.Models.Dtos.Summaries;
using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.Dtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    public ItemSummaryDto Item { get; set; } = new();
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public List<BidSummaryDto> Bids { get; set; } = [];
    public CharacterSummaryDto? Winner { get; set; }
    public AuctionStatus Status { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
}
