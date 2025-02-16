using System;

namespace TLMaster.Core.Entities;

public class Party(Guid id, string name, List<Character> characters)
    : BaseEntity(id)
{
    public string Name { get; set; } = name;

    public List<Character> Characters { get; set; } = characters;

}
