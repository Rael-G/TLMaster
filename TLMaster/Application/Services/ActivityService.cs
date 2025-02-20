using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class ActivityService(IActivityRepository activityRepository, IMapper mapper) 
    : BaseService<ActivityDto, Activity>(activityRepository, mapper), IActivityService
{

}
