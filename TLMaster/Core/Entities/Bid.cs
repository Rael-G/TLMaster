using System;

namespace TLMaster.Core.Entities;

public class Bid : BaseEntity, IComparable<Bid>
{
    private int _amount;

    public Character Bidder { get; set; }

    public Guid BidderId { get; set; }

    public Auction Auction { get; set; }

    public Guid AuctionId { get; set; }

    public int Amount 
    { 
        get => _amount; 
        set
        {
            ValidateValue(value);
            _amount = value;
        }  
    }

    public Bid(Guid id, Character bidder, Auction auction, int amount)
        : base(id)
    {
        Bidder = bidder;
        Auction = auction;
        Amount = amount;
    }
// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Bid()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }

    public int CompareTo(Bid? other)
    {
        return Amount.CompareTo(other?.Amount);
    }

    private static void ValidateValue(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(Amount));
    }
}
