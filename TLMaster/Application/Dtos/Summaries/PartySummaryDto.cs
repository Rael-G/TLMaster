using System;

namespace TLMaster.Application.Dtos.Summaries;

public class PartySummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public List<CharacterSummaryDto> Characters { get; set; } = [];
}
