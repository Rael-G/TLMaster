using System;

namespace TLMaster.Application.Dtos.Summaries;

public class GuildSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CharacterSummaryDto GuildMaster { get; set; } = new();
}
