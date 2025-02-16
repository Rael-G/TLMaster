using System;

namespace TLMaster.Core.Entities;

public class BaseEntity(Guid id)
{
    private Guid _id = id;

    public Guid Id 
    { 
        get => _id; 
        set
        {
            ValidateId(value);
            _id = value;
        }
    }
    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException($"Id must not be empty.");
    }
}
