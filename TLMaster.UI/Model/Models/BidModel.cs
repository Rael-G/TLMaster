namespace TLMaster.UI.Model.Models;

public class BidModel
{
    public string Id { get; set; } = string.Empty;
    public CharacterModel Bidder { get; set; } = new();
    public AuctionModel Auction { get; set; } = new();
    public int Amount { get; set; }
}
