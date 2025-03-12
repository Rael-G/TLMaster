using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class BalanceDto : IDto
{
    public Guid Id { get; set; }
    
    public GuildSummaryDto Guild { get; set; } = new();

    public Guid GuildId { get; set; }

    public CharacterSummaryDto Character { get; set; } = new();

    public Guid CharacterId { get; set; }

    public int Amount { get; set; }
}
