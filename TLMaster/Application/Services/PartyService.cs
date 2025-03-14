using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;


public class PartyService(IPartyRepository partyRepository, IMapper mapper, ICharacterRepository characterRepository)
    : BaseService<PartyDto, Party>(partyRepository, mapper), IPartyService
{
    private readonly IPartyRepository _partyRepository = partyRepository;
    private readonly ICharacterRepository _characterRepository = characterRepository;
    
    public override async Task Update(PartyDto partyDto, Guid authenticatedUserId)
    {
        var party = await _partyRepository.GetById(partyDto.Id, true);
        party = Mapper.Map(partyDto, party);
        List<Character> characters = [];
        foreach(var character in partyDto.Characters)
        {
            var member = await _characterRepository.GetById(character.Id, true);
            if (member != null) characters.Add(member);
        }

        if (party is not null)
        {
            party.Characters = characters;
        
            _partyRepository.Update(party);
            await _partyRepository.Commit();
        }
    }
}