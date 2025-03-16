using System;
using TLMaster.Core.Enums;

namespace TLMaster.Application.Dtos.Summaries;

public class AuctionSummaryDto 
{ 
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public int BidStep { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AuctionStatus Status { get; set; }
    public Guid GuildId { get; set; }
}