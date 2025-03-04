using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class GuildService(IGuildRepository guildRepository, IMapper mapper)
    : BaseService<GuildDto, Guild>(guildRepository, mapper), IGuildService
{
    private readonly IGuildRepository _guildRepository = guildRepository;

    public override async Task Update(GuildDto guildDto, Guid authenticatedUserId)
    {
        var guild = Mapper.Map<Guild>(guildDto);
        var staffIds = guildDto.Staff.Select(s => s.Id).ToList();
        await _guildRepository.Update(guild, staffIds);
        await Repository.Commit();
    }
}
