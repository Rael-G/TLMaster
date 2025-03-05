using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Mappings;

public class DomainToDto : Profile
{
    public DomainToDto()
    {
        CreateMap<Auction, AuctionDto>()
            .ReverseMap();

         CreateMap<Auction, AuctionSummaryDto>()
            .ReverseMap();

        CreateMap<Bid, BidDto>()
            .ReverseMap();

        CreateMap<Bid, BidSummaryDto>()
            .ReverseMap();

        CreateMap<Character, CharacterDto>()
            .ReverseMap();

        CreateMap<Character, CharacterSummaryDto>()
            .ReverseMap();

        CreateMap<Guild, GuildDto>()
            .ReverseMap();

        CreateMap<Guild, GuildSummaryDto>()
            .ReverseMap();

        CreateMap<Item, ItemDto>()
            .ReverseMap();

        CreateMap<Item, ItemSummaryDto>()
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ReverseMap();

        CreateMap<Party, PartySummaryDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<User, UserSummaryDto>()
            .ReverseMap();

        CreateMap<Activity, ActivityDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Activity, ActivitySummaryDto>()
            .ReverseMap();
    }
}
