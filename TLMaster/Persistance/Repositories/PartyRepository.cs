using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;

namespace TLMaster.Persistance.Repositories;

public class PartyRepository(ApplicationDbContext context)
    : BaseRepository<Party>(context), IPartyRepository
{

}
