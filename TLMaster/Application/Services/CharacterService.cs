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
}
