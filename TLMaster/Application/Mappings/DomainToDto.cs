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
            .ReverseMap()
            .ForMember(dest => dest.Item, opt => opt.Ignore())
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

         CreateMap<Auction, AuctionSummaryDto>()
            .ReverseMap();

        CreateMap<Bid, BidDto>()
            .ReverseMap()
            .ForMember(dest => dest.Bidder, opt => opt.Ignore())
            .ForMember(dest => dest.Auction, opt => opt.Ignore());

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
            .ReverseMap()
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

        CreateMap<Item, ItemSummaryDto>()
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ReverseMap()
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

        CreateMap<Party, PartySummaryDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<User, UserSummaryDto>()
            .ReverseMap();

        CreateMap<Activity, ActivityDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

        CreateMap<Activity, ActivitySummaryDto>()
            .ReverseMap();

        CreateMap<Balance, BalanceDto>()
            .ReverseMap()
            .ForMember(dest => dest.Character, opt => opt.Ignore())
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

         CreateMap<Balance, BalanceSummaryDto>();
    }
}
