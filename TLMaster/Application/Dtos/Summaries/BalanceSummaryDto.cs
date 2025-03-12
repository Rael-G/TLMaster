using System;

namespace TLMaster.Application.Dtos.Summaries;

public class BalanceSummaryDto
{
    public Guid GuildId { get; set; }

    public Guid CharacterId { get; set; }

    public int Amount { get; set; }
}
