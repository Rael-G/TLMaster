using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;

namespace TLMaster.Persistance.Repositories;

public class BidRepository(ApplicationDbContext context) 
    : BaseRepository<Bid>(context), IBidRepository
{

}
