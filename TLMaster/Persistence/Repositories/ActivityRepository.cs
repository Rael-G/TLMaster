using System;
using Microsoft.EntityFrameworkCore;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class ActivityRepository(ApplicationDbContext context) 
    : BaseRepository<Activity>(context), IActivityRepository
{
    public async Task Update(Activity activity, List<Guid> characterIds)
    {
        var characters = await Context.Characters
        .Where(c => characterIds.Contains(c.Id))
        .ToListAsync();

        activity.Participants = characters;

        Context.Update(activity);
    }
}
