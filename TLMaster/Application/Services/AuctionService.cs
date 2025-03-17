using AutoMapper;
using Quartz;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Application.Jobs;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class AuctionService(IAuctionRepository auctionRepository, IMapper mapper, ISchedulerFactory schedulerFactory)
    : BaseService<AuctionDto, Auction>(auctionRepository, mapper), IAuctionService
{
    private readonly ISchedulerFactory _schedulerFactory = schedulerFactory;

    public override async Task Create(AuctionDto dto, Guid authenticatedUserId)
    {
        await base.Create(dto, authenticatedUserId);
        await ScheduleAuctionJobs(dto);
    }
    
    public async Task ScheduleAuctionJobs(AuctionDto auction)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var startJob = JobBuilder.Create<StartAuctionJob>()
            .UsingJobData("AuctionId", auction.Id)
            .WithIdentity($"StartAuction-{auction.Id}")
            .Build();

        var startTrigger = TriggerBuilder.Create()
            .StartAt(auction.StartTime)
            .WithSimpleSchedule(x => x
                .WithMisfireHandlingInstructionFireNow()
            )
            .Build();

        await scheduler.ScheduleJob(startJob, startTrigger);

        var endJob = JobBuilder.Create<FinishAuctionJob>()
            .UsingJobData("AuctionId", auction.Id)
            .WithIdentity($"EndAuction-{auction.Id}")
            .Build();

        var endTrigger = TriggerBuilder.Create()
            .StartAt(auction.StartTime + auction.Duration)
            .WithSimpleSchedule(x => x
                .WithMisfireHandlingInstructionFireNow()
            )
            .Build();

        await scheduler.ScheduleJob(endJob, endTrigger);
    }
}