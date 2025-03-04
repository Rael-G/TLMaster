using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class ActivityDto
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
    public List<CharacterSummaryDto> Participants { get; set; } = [];
}
