using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class BidDto : IDto
{
    public Guid Id { get; set; }
    public CharacterSummaryDto Bidder { get; set; } = new();
    public Guid BidderId { get; set; }
    public AuctionSummaryDto Auction { get; set; } = new();
    public Guid AuctionId { get; set; }
    public int Amount { get; set; }
}
