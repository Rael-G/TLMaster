using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CharacterSummaryDto? Owner { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
}
