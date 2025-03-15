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
        var party = await _partyRepository.GetByIdFull(partyDto.Id, true);
        party = Mapper.Map(partyDto, party);

        if (party is not null)
        {
            party.Characters.RemoveAll(c => !partyDto.Characters.Select(c => c.Id).Contains(c.Id));

            var newMembers = await _characterRepository.Get(p => 
                partyDto.Characters
                .Select(ps => ps.Id)
                .Contains(p.Id) && 
                !party.Characters
                .Select(ps => ps.Id)
                .Contains(p.Id));
            
            party.Characters.AddRange(newMembers);

            _partyRepository.Update(party);
            await _partyRepository.Commit();
        }
    }
}