using Quartz;
using TLMaster.Core.Enums;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Jobs;

public class FinishAuctionJob(IAuctionRepository auctionRepository, IItemRepository itemRepository) : IJob
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        if (!Guid.TryParse(context.JobDetail.JobDataMap.GetString("AuctionId"), out var auctionId))
            return;
        
        var auction = (await _auctionRepository.Get
            (filter: a => a.Id == auctionId && 
            a.Status == AuctionStatus.Active, includeProperties: "Bids", track: true))
            .FirstOrDefault();

        if (auction == null)
            return;

        auction.FinishAuction();

        var item = (await _itemRepository.Get(filter: i => i.Id == auction.ItemId, track: true)).FirstOrDefault();
        if (item != null)
            item.OwnerId = auction.WinnerId;

        await _auctionRepository.Commit();
    }
}
