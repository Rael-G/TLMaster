using TLMaster.Core.Enums;

namespace TLMaster.Core.Entities;

public class Character : BaseEntity
{
    public string Name { get; set; }

    public Guild? Guild { get; set; }

    public Guid? GuildId { get; set; }

    public List<Item> Itens { get; set; } = [];

    public Balance? Balance { get; set; }

    public Role Role { get; set; }

    public List<Weapon> Weapons { get; set; }

    public List<Bid> Bids { get; set; } = [];

    public User User { get; private set; }

    public Guid UserId { get; private set; }

    public Party? Party { get; set; }

    public Guid? PartyId { get; set; }

    public List<Activity> Activities { get; set; } = [];

    public List<Guild> Applications { get; set; } = [];

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Character()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
