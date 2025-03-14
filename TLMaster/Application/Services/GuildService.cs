using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class GuildService(IGuildRepository guildRepository, UserManager<User> userManager, IMapper mapper)
    : BaseService<GuildDto, Guild>(guildRepository, mapper), IGuildService
{
    private readonly IGuildRepository _guildRepository = guildRepository;
    private readonly UserManager<User> _userManager = userManager;

    public override async Task Update(GuildDto guildDto, Guid authenticatedUserId)
    {
        var guild = await _guildRepository.GetById(guildDto.Id, true);
        guild = Mapper.Map(guildDto, guild);
        List<User> users = [];
        foreach(var user in guildDto.Staff)
        {
            var staff = await _userManager.FindByIdAsync(user.Id.ToString());
            if (staff != null) users.Add(staff);
        }

        if (guild is not null)
        {
            guild.Staff = users;
        
            _guildRepository.Update(guild);
            await _guildRepository.Commit();
        }
    }

}
