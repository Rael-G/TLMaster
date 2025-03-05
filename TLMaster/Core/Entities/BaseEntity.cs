using System;

namespace TLMaster.Core.Entities;

public class BaseEntity
{
    private Guid _id;

    public Guid Id 
    { 
        get => _id; 
        protected set
        {
            ValidateId(value);
            _id = value;
        }
    }

    public BaseEntity()
    {
        
    }

    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException($"Id must not be empty.");
    }
}
