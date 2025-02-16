using System;
using TLMaster.Enums;

namespace TLMaster.Entities;

public class Character(Guid id, string name, Guild guild, List<Item> itens, int coin, Role role, List<Weapon> weapons)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    public Guild? Guild { get; set; } = guild;

    public List<Item> Itens { get; set; } = itens;

    public int Coin { get; set; } = coin;

    public Role Role { get; set; } = role;

    public List<Weapon> Weapons { get; set; } = weapons;
}
