using System;
using Microsoft.AspNetCore.Identity;

namespace TLMaster.Core.Entities;

public class User : IdentityUser<Guid>
{
    public List<Character> Characters { get; set; } = [];

    public List<Guild> OwnedGuilds { get; set; } = [];

    public List<Guild> StaffGuilds { get; set; } = [];

    public User()
    {

    }

}
