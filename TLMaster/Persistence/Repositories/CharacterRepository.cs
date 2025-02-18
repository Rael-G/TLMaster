using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class CharacterRepository(ApplicationDbContext context)
    : BaseRepository<Character>(context), ICharacterRepository
{

}
