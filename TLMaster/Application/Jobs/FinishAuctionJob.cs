using Quartz;
using TLMaster.Core.Enums;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Jobs;

public class FinishAuctionJob(IAuctionRepository auctionRepository) : IJob
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        var auctionId = context.JobDetail.JobDataMap.GetGuid("AuctionId");
        
        var auction = (await _auctionRepository.Get
            (filter: a => a.Id == auctionId && 
            a.Status == AuctionStatus.Active,  track: true))
            .FirstOrDefault();

        if (auction == null)
            return;

        auction.FinishAuction();

        await _auctionRepository.Commit();
    }
}
