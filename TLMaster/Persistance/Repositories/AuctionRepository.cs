using System;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;
using TLMaster.Persistance.Contexts;

namespace TLMaster.Persistance.Repositories;

public class AuctionRepository(ApplicationDbContext context) 
    : BaseRepository<Auction>(context), IAuctionRepository
{

}
