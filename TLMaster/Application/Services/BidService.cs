using System;
using AutoMapper;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class BidService(IBidRepository bidRepository, IMapper mapper)
    : BaseService<BidDto, Bid>(bidRepository, mapper), IBidService
{

}

