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
            .ForMember(dest => dest.WinnerId, opt => opt.MapFrom(src => src.Winner != null? src.Winner.Id : (Guid?)null))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Item.Id))
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild.Id));

         CreateMap<Auction, AuctionSummaryDto>()
            .ReverseMap();

        CreateMap<Bid, BidDto>()
            .ReverseMap()
            .ForMember(dest => dest.BidderId, opt => opt.MapFrom(src => src.Bidder.Id))
            .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.Auction.Id));

        CreateMap<Bid, BidSummaryDto>()
            .ReverseMap();

        CreateMap<Character, CharacterDto>()
            .ReverseMap()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild != null? src.Guild.Id : (Guid?)null))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        
        CreateMap<Character, CharacterSummaryDto>()
            .ReverseMap();

        CreateMap<Guild, GuildDto>()
            .ReverseMap()
            .ForMember(dest => dest.GuildMasterId, opt => opt.MapFrom(src => src.GuildMaster.Id));

        CreateMap<Guild, GuildSummaryDto>()
            .ReverseMap();

        CreateMap<Item, ItemDto>()
            .ReverseMap()
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner != null? src.Owner.Id : (Guid?)null))
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild.Id));

        CreateMap<Item, ItemSummaryDto>()
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ReverseMap()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild.Id));

        CreateMap<Party, PartySummaryDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<User, UserSummaryDto>()
            .ReverseMap();

        CreateMap<Activity, ActivityDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild.Id));

        CreateMap<Activity, ActivitySummaryDto>()
            .ReverseMap();
    }
}
