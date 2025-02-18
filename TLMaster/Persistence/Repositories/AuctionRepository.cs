using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistence.Contexts;

namespace TLMaster.Persistence.Repositories;

public class AuctionRepository(ApplicationDbContext context) 
    : BaseRepository<Auction>(context), IAuctionRepository
{

}
