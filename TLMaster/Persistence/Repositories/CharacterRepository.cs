using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class CharacterRepository(ApplicationDbContext context)
    : BaseRepository<Character>(context), ICharacterRepository
{
    public async Task Update(Character character, List<Guid> guildIds)
    {
        var applcations = await Context.Guilds
        .Where(g => guildIds.Contains(g.Id))
        .ToListAsync();

        character.Applications = applcations;

        Context.Update(character);
    }
}
