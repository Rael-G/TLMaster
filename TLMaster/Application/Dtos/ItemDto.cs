using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class ItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CharacterSummaryDto? Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
    public Guid GuildId { get; set; }
}

