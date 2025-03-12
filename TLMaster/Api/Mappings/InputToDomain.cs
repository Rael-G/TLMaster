using System;
using AutoMapper;
using TLMaster.Api.Models.InputModels;
using TLMaster.Application.Dtos;
using TLMaster.Application.Dtos.Summaries;

namespace TLMaster.Api.Mappings;

public class InputToDto : Profile
{
    public InputToDto()
    {
        CreateMap<CharacterInputModel, CharacterDto>()
            .ForMember(dest => dest.Applications, opt => opt.MapFrom(src => src.ApplicationIds.Select(id => new GuildSummaryDto() { Id = id })));

        CreateMap<PartyInputModel, PartyDto>()
            .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.CharacterIds.Select(id => new CharacterSummaryDto() { Id = id })));

        CreateMap<ItemInputModel, ItemDto>();

        CreateMap<GuildInputModel, GuildDto>()
            .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.StaffIds.Select(id => new UserSummaryDto() { Id = id })));

        CreateMap<BidInputModel, BidDto>();

        CreateMap<AuctionInputModel, AuctionDto>();

        CreateMap<ActivityInputModel, ActivityDto>();
    }
}
