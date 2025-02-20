using System;

namespace TLMaster.Core.Entities;

public class Activity : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateTime { get; set; }

    public bool IsPasswordRequired { get; set; }

    public string? Password { get; set; }

    public List<Character> Participants { get; set; } = [];

    public Activity(Guid id, string name, string description, DateTime dateTime, bool isPasswordRequired, string? password)
        : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
        DateTime = dateTime;
        IsPasswordRequired = isPasswordRequired;
        Password = password;
    }

    // Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Activity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
}
