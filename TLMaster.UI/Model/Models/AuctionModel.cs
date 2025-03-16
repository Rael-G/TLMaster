using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Model.Models;

public class AuctionModel
{
    public string Id { get; set; } = string.Empty;
    public ItemModel Item { get; set; } = new();
    public string ItemId { get; set; } = string.Empty;
    public int BidStep { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public List<BidModel> Bids { get; set; } = [];
    public CharacterModel? Winner { get; set; }
    public string WinnerId { get; set; } = string.Empty;
    public AuctionStatus Status { get; set; }
    public GuildModel Guild { get; set; } = new();
    public string GuildId { get; set; } = string.Empty;
}
