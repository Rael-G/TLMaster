using System;

namespace TLMaster.Core.Entities;

public class Party : BaseEntity
{
    public string Name { get; set; }

    public List<Character> Characters { get; set; } = [];

    public Guild Guild { get; set; }

    public Guid GuildId { get; set; }

    public Party(Guid id, string name, Guild guild) : base(id)
    {
        Name = name;
        Guild = guild;
    }

// Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Party()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
