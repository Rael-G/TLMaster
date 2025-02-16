using System;
using TLMaster.Enums;

namespace TLMaster.Entities;

public class Auction(Item item, int initialPrice, DateTime startTime, TimeSpan duration, List<Bid> bids, Character? winner, AuctionStatus status)
{
    public Item Item { get; set; } = item;

    public int InitialPrice { get; set; } = initialPrice;

    public DateTime StartTime { get; set; } = startTime;

    public TimeSpan Duration { get; set; } = duration;

    public List<Bid> Bids { get; private set; } = bids;

    public Character? Winner { get; set; } = winner;

    public AuctionStatus Status { get; set; } = status;

    public Bid? HighestBid => Bids.Count > 0 ? Bids.Max() : null;

    public void AddBid(Bid bid)
    {
        if (Status is not AuctionStatus.Active)
            throw new ArgumentException("The auction isn't active.");

        if (HighestBid is not null && bid.Value <= HighestBid.Value)
            throw new ArgumentException("New bid should be greater than the last bid on this auction.");

        Bids.Add(bid);
    }

    public void ValidateInitialPrice(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(InitialPrice));
    }
}