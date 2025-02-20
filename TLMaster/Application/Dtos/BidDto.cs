using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class BidDto : IDto
{
    public Guid Id { get; set; }
    public Guid BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public int Amount { get; set; }
}
