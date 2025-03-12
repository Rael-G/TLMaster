using System;

namespace TLMaster.Core.Entities;

public class Balance : BaseEntity
{
    public Guild Guild { get; set; }

    public Guid GuildId { get; set; }

    public Character Character { get; set; }

    public Guid CharacterId { get; set; }

    public int Amount { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Balance()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
