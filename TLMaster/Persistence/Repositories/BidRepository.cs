using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class BidRepository(ApplicationDbContext context) 
    : BaseRepository<Bid>(context), IBidRepository
{

}
