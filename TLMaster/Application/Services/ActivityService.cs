using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;
using TLMaster.Core.Interfaces.Repositories;

namespace TLMaster.Application.Services;

public class ActivityService(IActivityRepository activityRepository, ICharacterRepository characterRepository, IMapper mapper) 
    : BaseService<ActivityDto, Activity>(activityRepository, mapper), IActivityService
{
    private readonly IActivityRepository _activityRepository = activityRepository;
    private readonly ICharacterRepository _characterRepository = characterRepository;

    public async Task Participate(Guid activityId, Guid characterId, string password , Guid authenticatedUserId)
    {
        var activity = await _activityRepository.GetByIdFull(activityId, true);

        if (activity is not null && activity.Password == password)
        {
            var character = await _characterRepository.GetByIdFull(characterId, true);
            
            if (character != null)
            {
                activity.Participants.Add(character);
                character.Balance!.Amount += activity.Payout;
                await _activityRepository.Commit();
            }
        }
    }
}
