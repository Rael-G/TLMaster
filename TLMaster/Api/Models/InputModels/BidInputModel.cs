using System;
using TLMaster.Api.Interfaces;
using TLMaster.Application;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class BidInputModel : IInputModel<BidDto>
{
    public Guid BidderId { get; set; }
    public Guid AuctionId { get; set; }
    public int Value { get; set; }

    public BidDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            BidderId = BidderId,
            AuctionId = AuctionId,
            Value = Value
        };

    public void InputToDto(BidDto dto)
    {
        dto.BidderId = BidderId;
        dto.AuctionId = AuctionId;
        dto.Value = Value;
    }
}
