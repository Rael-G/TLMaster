using System;

namespace TLMaster.Entities;

public class Item(Guid id, string name, Character? owner)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    public Character? Owner { get; set; } = owner;
}
