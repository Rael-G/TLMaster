using System;
using TLMaster.Core.Entities;

namespace TLMaster.Core.Interfaces.Repositories;

public interface IGuildRepository : IBaseRepository<Guild>
{
    Task Update(Guild guild, List<Guid> staffIds);
}
