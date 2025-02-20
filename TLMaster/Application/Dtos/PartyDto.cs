using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class PartyDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public Guid GuildId { get; set; }
}
