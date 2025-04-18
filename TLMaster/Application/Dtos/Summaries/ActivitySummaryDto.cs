using System;

namespace TLMaster.Application.Dtos.Summaries;

public class ActivitySummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Payout { get; set; }
    public DateTime DateTime { get; set; }
    public string Password { get; set; } = string.Empty;
}
