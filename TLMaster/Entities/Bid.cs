using System;

namespace TLMaster.Entities;

public class Bid : IComparable<Bid>
{
    public Bid(Character bidder, Auction auction, int value)
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

    public void ValidateValue(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(Value));
    }
}
