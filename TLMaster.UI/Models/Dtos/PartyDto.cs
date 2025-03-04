using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class PartyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CharacterSummaryDto> Characters { get; set; } = [];
    public GuildSummaryDto Guild { get; set; } = new();
}
