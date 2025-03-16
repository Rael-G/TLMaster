using System;
using TLMaster.Application.Dtos;

namespace TLMaster.Application.Interfaces;

public interface IActivityService : IBaseService<ActivityDto>
{
    Task Participate(Guid activityId, Guid characterId, string password , Guid authenticatedUserId);
}
