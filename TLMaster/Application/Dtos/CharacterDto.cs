using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Enums;

namespace TLMaster.Application.Dtos;

public class CharacterDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public GuildSummaryDto? Guild { get; set; }
    public Guid? GuildId { get; set; }
    public List<ItemSummaryDto> Items { get; set; } = [];
    public BalanceSummaryDto Balance { get; set; } = new();
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public UserSummaryDto User { get; set; } = new();
    public Guid UserId { get; set; }
    public List<ActivitySummaryDto> Activities { get; set; } = [];
    public List<GuildSummaryDto> Applications { get; set; } = [];
}
