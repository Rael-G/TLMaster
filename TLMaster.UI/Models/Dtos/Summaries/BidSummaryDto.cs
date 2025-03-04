using System;

namespace TLMaster.UI.Models.Dtos.Summaries;

public class BidSummaryDto 
{ 
    public Guid Id { get; set; } 
    public int Amount { get; set; }
    public CharacterSummaryDto Bidder { get; set; } = new();
}
