using System;
using AutoMapper;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class GuildService(IGuildRepository guildRepository, IMapper mapper)
    : BaseService<GuildDto, Guild>(guildRepository, mapper), IGuildService
{
}
