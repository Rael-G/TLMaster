using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class GuildService(IGuildRepository guildRepository, ICharacterRepository characterRepository, IBalanceRepository balanceRepository, UserManager<User> userManager, IMapper mapper)
    : BaseService<GuildDto, Guild>(guildRepository, mapper), IGuildService
{
    private readonly IGuildRepository _guildRepository = guildRepository;
    private readonly ICharacterRepository _characterRepository = characterRepository;
    private readonly IBalanceRepository _balanceRepository = balanceRepository;
    private readonly UserManager<User> _userManager = userManager;

    public override async Task Update(GuildDto guildDto, Guid authenticatedUserId)
    {
        var guild = await _guildRepository.GetByIdFull(guildDto.Id, true);
        guild = Mapper.Map(guildDto, guild);

        if (guild != null)
        {
            guild.Staff.RemoveAll(c => !guildDto.Staff.Select(c => c.Id).Contains(c.Id));

            var newStaff = await _userManager.Users.Where(g => 
                guildDto.Staff
                .Select(gs => gs.Id)
                .Contains(g.Id) && 
                !guild.Staff
                .Select(gs => gs.Id)
                .Contains(g.Id))
                .ToListAsync();
            
            guild.Staff.AddRange(newStaff);

            _guildRepository.Update(guild);
            await _guildRepository.Commit();
        }
    }

    public async Task AcceptMember(Guid guildId, Guid applicantId, Guid authenticatedUserId)
    {
        var character = await _characterRepository.GetByIdFull(applicantId, track: true);

        if (character != null)
        {
            var guild = character.Applications.Where(g => g.Id == guildId).FirstOrDefault();
            if (guild != null)
            {
                character.Applications.Remove(guild);
                character.Guild = guild;
                _characterRepository.Update(character);
                var balanceDto = new BalanceDto()
                {
                    Id = Guid.NewGuid(),
                    GuildId = guildId,
                    CharacterId = character.Id
                };

                _balanceRepository.Create(Mapper.Map<Balance>(balanceDto));
                await _balanceRepository.Commit();
            }
        }
    }

    public async Task RejectMember(Guid guildId, Guid applicantId, Guid authenticatedUserId)
    {
        var character = await _characterRepository.GetByIdFull(applicantId, true);
        
        if (character != null)
        {
            var guild = character.Applications.Where(g => g.Id == guildId).FirstOrDefault();
            if (guild != null) character.Applications.Remove(guild);

            _characterRepository.Update(character);
            await _characterRepository.Commit();
        }
    }

    public async Task RemoveMember(Guid guildId, Guid memberId, Guid authenticatedUserId)
    {
        var guild = await _guildRepository.GetByIdFull(guildId, true);
        if (guild?.Members.Where(m => m.Id == memberId).FirstOrDefault() != null)
        {
            var member = guild.Members.Where(m => m.Id == memberId).FirstOrDefault();
            if (member != null)
            {
                member.Guild = null;
                var balanceId = guild.Balances.Where(b => b.CharacterId == member.Id).FirstOrDefault();
                if (balanceId != null)
                {
                    _balanceRepository.Delete(balanceId);
                }
                await _characterRepository.Commit();
            }
        }
    }
}
