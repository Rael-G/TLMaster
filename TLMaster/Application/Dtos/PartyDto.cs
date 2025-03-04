using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class PartyDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CharacterSummaryDto> Characters { get; set; } = [];
    public GuildSummaryDto Guild { get; set; } = new();
}
