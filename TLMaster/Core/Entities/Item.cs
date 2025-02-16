using System;

namespace TLMaster.Core.Entities;

public class Item(Guid id, string name, Character? owner)
    : BaseEntity(id)
{
    public string Name { get; set; } = name;

    public Character? Owner { get; set; } = owner;
}
