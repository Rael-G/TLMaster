using TLMaster.Core.Enums;
using TLMaster.Persistence.Migrations;

namespace TLMaster.Core.Entities;

public class Auction : BaseEntity
{
    private int _bidStep;

    public Item Item { get; set; }

    public Guid ItemId { get; set; }

    public int BidStep
    {
        get => _bidStep;
        set
        {
            ValidateInitialPrice(value);
            _bidStep = value;
        }
    }

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public List<Bid> Bids { get; private set; } = [];

    public Character? Winner { get; set; }

    public Guid? WinnerId { get; set; }

    public AuctionStatus Status { get; private set; }

    public Bid? HighestBid => Bids.Count > 0 ? Bids.Max() : null;

    public Guild Guild { get; set; }

    public Guid GuildId { get; set; }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Auction()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }

    public void ValidateBid(Bid bid)
    {
        if (Status is not AuctionStatus.Active)
            throw new ArgumentException("The auction isn't active.");

        if (HighestBid is not null && bid.Amount <= HighestBid.Amount)
            throw new ArgumentException("New bid should be greater than the last bid on this auction.");

        if (bid.Amount < BidStep || bid.Amount % BidStep != 0)
            throw new ArgumentException("New Bid should Respect bid step.");
    }

    public void StartAuction()
    {
        if (Status == AuctionStatus.Pending)
            Status = AuctionStatus.Active;
        else
            throw new Exception("An auction that is not pending should not be started.");
    }

    public void FinishAuction()
    {
        if (Status == AuctionStatus.Active)
        {
            Status = AuctionStatus.Finished;
            Winner = HighestBid?.Bidder;
        }
        else
        {
            throw new Exception("An auction that is not active should not be finished.");
        }
    }

    public void CancelAuction()
    {
        if (Status != AuctionStatus.Finished)
            Status = AuctionStatus.Canceled;
        else
            throw new Exception("A finished auction should not be canceled.");
    }

    private static void ValidateInitialPrice(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(BidStep));
    }
}