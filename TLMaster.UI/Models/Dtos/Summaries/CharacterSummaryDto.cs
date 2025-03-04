using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.Dtos.Summaries;

public class CharacterSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; }
    public int Coin { get; set; }
    public UserSummaryDto User { get; set; } = new();
}

