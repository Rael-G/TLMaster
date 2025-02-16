using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Context;

namespace TLMaster.Persistance.Repositories;

public class CharacterRepository(ApplicationDbContext context)
    : BaseRepository<Character>(context), ICharacterRepository
{

}
