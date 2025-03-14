using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class CharacterService(ICharacterRepository characterRepository, IMapper mapper, IGuildRepository guildRepository)
    : BaseService<CharacterDto, Character>(characterRepository, mapper), ICharacterService
{
    private readonly ICharacterRepository _characterRepository = characterRepository;
    private readonly IGuildRepository _guildRepository = guildRepository;
    
    public override async Task Update(CharacterDto characterDto, Guid authenticatedUserId)
    {
        var character = await _characterRepository.GetByIdFull(characterDto.Id, true);
        character = Mapper.Map(characterDto, character);
        List<Guild> applications = [];
        foreach(var guild in characterDto.Applications)
        {
            var application = await _guildRepository.GetById(guild.Id, true);
            if (application != null) applications.Add(application);
        }

        if (character is not null)
        {
            character.Applications = applications;
        
            _characterRepository.Update(character);
            await _characterRepository.Commit();
        }
    }
}
