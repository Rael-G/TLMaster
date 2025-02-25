using TLMaster.UI.Enums;

namespace TLMaster.UI.Dtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public List<Guid> BidIds { get; set; } = [];
    public Guid? WinnerId { get; set; }
    public AuctionStatus Status { get; set; }
    public Guid GuildId { get; set; }
}
