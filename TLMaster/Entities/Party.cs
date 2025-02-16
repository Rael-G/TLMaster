using System;

namespace TLMaster.Entities;

public class Party(Guid id, string name, List<Character> characters)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    public List<Character> Characters { get; set; } = characters;

}
