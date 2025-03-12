using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class BalanceService(IBalanceRepository balanceRepository, IMapper mapper)
    : BaseService<BalanceDto, Balance>(balanceRepository, mapper), IBalanceService
{

}
