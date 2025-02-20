using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class GuildRepository(ApplicationDbContext context)
    : BaseRepository<Guild>(context), IGuildRepository
{
    public virtual async Task Update(Guild guild, List<Guid> staffIds)
    {
        var staff = await Context.Users
        .Where(c => staffIds.Contains(c.Id))
        .ToListAsync();

        guild.Staff = staff;

        Context.Update(guild);
    }
}
