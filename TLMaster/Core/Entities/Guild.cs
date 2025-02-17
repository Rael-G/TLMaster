using System;

namespace TLMaster.Core.Entities;

public class Guild : BaseEntity
{
    public User GuildMaster { get; set; }

    public Guid GuildMasterId { get; set; }

    public List<User> Staff { get; set; } = [];

    public List<Character> Characters { get; set; } = [];

    public List<Auction> Auctions { get; set; } = [];

    public List<Party> Parties { get; set; } = [];

    public Guild(Guid id, User guildMaster) : base(id)
    {
        GuildMaster = guildMaster;
    }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Guild()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }

}
