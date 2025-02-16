using System;

namespace TLMaster.Core.Entities;

public class Guild(Guid id, User guildMaster, List<User> staff, List<Character> characters, List<Auction> auctions, List<Party> parties)
{
    public Guid Id { get; set; } = id;

    public User GuildMaster { get; set; } = guildMaster;

    public List<User> Staff { get; set; } = staff;

    public List<Character> Characters { get; set; } = characters;

    public List<Auction> Auctions { get; set; } = auctions;

    public List<Party> Parties { get; set; } = parties;

}
