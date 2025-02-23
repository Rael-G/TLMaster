using System;

namespace TLMaster.Core.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; }

    public Auction? Auction { get; set; }

    public Character? Owner { get; set; }

    public Guid? OwnerId { get; set; }

    public Guild Guild { get; set; }

    public Guid GuildId { get; set; }

    public Item(Guid id, string name, Guild guild) : base(id)
    {
        Name = name;
        Guild = guild;
        GuildId = guild.Id;
    }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Item()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
