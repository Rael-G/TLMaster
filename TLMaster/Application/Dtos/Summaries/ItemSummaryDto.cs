using System;

namespace TLMaster.Application.Dtos.Summaries;

public class ItemSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public CharacterSummaryDto? Owner { get; set; }
}
