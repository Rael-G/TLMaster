using TLMaster.Core.Enums;

namespace TLMaster.Core.Entities;

public class Auction : BaseEntity
{
    private int _initialPrice;

    public Item Item { get; set; }

    public Guid ItemId { get; set; }

    public int InitialPrice
    {
        get => _initialPrice;
        set
        {
            ValidateInitialPrice(value);
            _initialPrice = value;
        }
    }

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public List<Bid> Bids { get; private set; } = [];

    public Character? Winner { get; set; }

    public Guid? WinnerId { get; set; }

    public AuctionStatus Status { get; set; }

    public Bid? HighestBid => Bids.Count > 0 ? Bids.Max() : null;

    public Guild Guild { get; set; }

    public Guid GuildId { get; set; }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Auction()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }

    public void AddBid(Bid bid)
    {
        if (Status is not AuctionStatus.Active)
            throw new ArgumentException("The auction isn't active.");

        if (HighestBid is not null && bid.Amount <= HighestBid.Amount)
            throw new ArgumentException("New bid should be greater than the last bid on this auction.");

        Bids.Add(bid);
    }

    private static void ValidateInitialPrice(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(InitialPrice));
    }
}