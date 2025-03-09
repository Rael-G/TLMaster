namespace TLMaster.UI.Models.InputModels;

public class BidInputModel
{
    public string BidderId { get; set; } = string.Empty;
    public string AuctionId { get; set; } = string.Empty;
    public int Amount { get; set; }
}
