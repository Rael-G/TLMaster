namespace TLMaster.UI.Models.Dtos;

public class BidDto
{
    public Guid Id { get; set; }
    public Guid BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public int Amount { get; set; }
}
