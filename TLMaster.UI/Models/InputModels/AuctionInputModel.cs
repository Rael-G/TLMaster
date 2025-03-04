
using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.InputModels;

public class AuctionInputModel
{
    public Guid ItemId { get; set; }
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid? WinnerId { get; set; }
    public AuctionStatus Status { get; set; }
    public Guid GuildId { get; set; }
}   
