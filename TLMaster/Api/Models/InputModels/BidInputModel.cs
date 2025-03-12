namespace TLMaster.Api.Models.InputModels;

public class BidInputModel
{
    public Guid BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public int Amount { get; set; }
}
