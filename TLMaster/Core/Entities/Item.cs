using System;

namespace TLMaster.Core.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; }

    public Auction? Auction { get; set; }

    public Character? Owner { get; set; }

    public Guid? OwnerId { get; set; }

    public Item(Guid id, string name) : base(id)
    {
        Name = name;
    }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Item()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
