using System;

namespace TLMaster.Core.Entities;

public class Bid : BaseEntity, IComparable<Bid>
{
    public Bid(Guid id, Character bidder, Auction auction, int value)
        : base(id)
    {
        Bidder = bidder;
        Auction = auction;
        Value = value;
    }
    private int _value;

    public Character Bidder { get; set; }

    public Auction Auction { get; set; }

    public int Value 
    { 
        get => _value; 
        set
        {
            ValidateValue(value);
            _value = value;
        }  
    }

    public int CompareTo(Bid? other)
    {
        return Value.CompareTo(other?.Value);
    }

    private static void ValidateValue(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(Value));
    }
}
