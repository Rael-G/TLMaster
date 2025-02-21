using System;

namespace TLMaster.Core.Entities;

public class RefreshToken : BaseEntity
{
    public string Token 
    { 
        get => Id.ToString(); 
        private set => Id = Guid.Parse(value); 
    }
    
    public DateTime ExpirationDate { get; set; }
    public User User { get; set;}
    public Guid UserId { get; set; }
    public bool IsRevoked { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public RefreshToken(User user, DateTime expirationDate)
    {
        Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
        ExpirationDate = expirationDate;
        CreatedAt = DateTime.UtcNow;
    }

    // Parameterless constructor for serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public RefreshToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }
}
