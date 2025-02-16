using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;

namespace TLMaster.Persistance.Repositories;

public class ItemRepository(ApplicationDbContext context)
    : BaseRepository<Item>(context), IItemRepository
{

}
