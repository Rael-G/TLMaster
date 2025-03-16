using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class BidService(IBidRepository bidRepository, IAuctionRepository auctionRepository, IMapper mapper)
    : BaseService<BidDto, Bid>(bidRepository, mapper), IBidService
{
    public override async Task Create(BidDto dto, Guid authenticatedUserId)
    {
        var bid = Mapper.Map<Bid>(dto);
        var auction = await auctionRepository.GetById(dto.AuctionId);
        auction?.ValidateBid(bid);
        await base.Create(dto, authenticatedUserId);
    }
}

