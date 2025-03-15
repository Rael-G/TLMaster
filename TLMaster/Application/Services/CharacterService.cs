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

        if (character != null)
        {
            character.Applications.RemoveAll(c => !characterDto.Applications.Select(c => c.Id).Contains(c.Id));

            var newApplications = await _guildRepository.Get(c => 
                characterDto.Applications
                .Select(cs => cs.Id)
                .Contains(c.Id) && 
                !character.Applications
                .Select(cs => cs.Id)
                .Contains(c.Id));

            character.Applications.AddRange(newApplications);

            _characterRepository.Update(character);
            await _characterRepository.Commit();
        }
    }
}
