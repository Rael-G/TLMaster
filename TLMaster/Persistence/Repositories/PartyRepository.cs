using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class PartyRepository(ApplicationDbContext context)
    : BaseRepository<Party>(context), IPartyRepository
{
    public async Task Update(Party party, List<Guid> characterIds)
    {
        var characters = await Context.Characters
        .Where(c => characterIds.Contains(c.Id))
        .ToListAsync();

        party.Characters = characters;

        Context.Update(party);
    }
}
