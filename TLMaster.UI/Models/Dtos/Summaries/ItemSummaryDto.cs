using System;

namespace TLMaster.UI.Models.Dtos.Summaries;

public class ItemSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public CharacterSummaryDto? Owner { get; set; }
}
