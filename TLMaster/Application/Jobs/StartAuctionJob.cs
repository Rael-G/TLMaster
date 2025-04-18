using Quartz;
using TLMaster.Core.Enums;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Jobs;

public class StartAuctionJob(IAuctionRepository auctionRepository) : IJob
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        if (!Guid.TryParse(context.JobDetail.JobDataMap.GetString("AuctionId"), out var auctionId))
            return;
        
        var auction = (await _auctionRepository.Get
            (filter: a => a.Id == auctionId && 
            a.Status == AuctionStatus.Pending,  track: true))
            .FirstOrDefault();

        if (auction == null)
            return;

        auction.StartAuction();

        await _auctionRepository.Commit();
    }
}
