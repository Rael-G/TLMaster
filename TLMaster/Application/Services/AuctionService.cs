using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class AuctionService(IAuctionRepository auctionRepository, IMapper mapper)
    : BaseService<AuctionDto, Auction>(auctionRepository, mapper), IAuctionService
{

}
