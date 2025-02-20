using System;
using TLMaster.Core.Entities;

namespace TLMaster.Core.Interfaces.Repositories;

public interface IPartyRepository : IBaseRepository<Party>
{
    Task Update(Party party, List<Guid> characterIds);
}
