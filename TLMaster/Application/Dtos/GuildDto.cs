using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class GuildDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid GuildMasterId { get; set; }
    public List<Guid> StaffIds { get; set; } = [];
    public List<Guid> CharacterIds { get; set; } = [];
    public List<Guid> AuctionIds { get; set; } = [];
    public List<Guid> PartyIds { get; set; } = [];
    public List<Guid> ItemIds { get; set; } = [];
}
