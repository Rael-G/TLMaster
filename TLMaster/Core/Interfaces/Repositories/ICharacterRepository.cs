using System;
using TLMaster.Core.Entities;

namespace TLMaster.Core.Interfaces.Repositories;

public interface ICharacterRepository : IBaseRepository<Character>
{
    Task Update(Character character, List<Guid> guildIds);
}
