using System;
using Microsoft.AspNetCore.Identity;

namespace TLMaster.Core.Entities;

public class User(List<Character> characters) : IdentityUser<Guid>
{
    public List<Character> Characters { get; set; } = characters;
}
