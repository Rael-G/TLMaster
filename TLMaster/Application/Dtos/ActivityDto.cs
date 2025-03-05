using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class ActivityDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Payout { get; set; }
    public bool WasPaid { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsPasswordRequired { get; set; }
    public string? Password { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
    public Guid GuildId { get; set; }
    public List<CharacterSummaryDto> Participants { get; set; } = [];
}
