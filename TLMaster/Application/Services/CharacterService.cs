using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class CharacterService(ICharacterRepository characterRepository, IMapper mapper)
    : BaseService<CharacterDto, Character>(characterRepository, mapper), ICharacterService
{
    private readonly ICharacterRepository _characterRepository = characterRepository;
    
    public override async Task Update(CharacterDto characterDto, Guid authenticatedUserId)
    {
        var character = Mapper.Map<Character>(characterDto);
        var guildIds = characterDto.Applications.Select(g => g.Id).ToList();
        await _characterRepository.Update(character, guildIds);
        await _characterRepository.Commit();
    }
}
