using System;

namespace TLMaster.Application.Dtos.Summaries;

public class BidSummaryDto 
{ 
    public Guid Id { get; set; } 
    public int Amount { get; set; }
    public Guid BidderId { get; set; }

}
