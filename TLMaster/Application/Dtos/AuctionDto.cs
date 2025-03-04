using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Enums;

namespace TLMaster.Application.Dtos;

public class AuctionDto : IDto
{
    public Guid Id { get; set; }
    public ItemSummaryDto Item { get; set; } = new();
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public List<BidSummaryDto> Bids { get; set; } = [];
    public CharacterSummaryDto? Winner { get; set; }
    public AuctionStatus Status { get; set; }
    public GuildSummaryDto Guild { get; set; } = new();
}
