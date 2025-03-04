using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.Dtos.Summaries;

public class AuctionSummaryDto 
{ 
    public Guid Id { get; set; } 
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AuctionStatus Status { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
}