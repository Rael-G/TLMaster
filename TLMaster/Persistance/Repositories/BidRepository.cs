using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Context;

namespace TLMaster.Persistance.Repositories;

public class BidRepository(ApplicationDbContext context) 
    : BaseRepository<Bid>(context), IBidRepository
{

}
