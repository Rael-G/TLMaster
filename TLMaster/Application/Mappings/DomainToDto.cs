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
            .ForMember(dest => dest.Bids, opt => opt.Ignore())
            .ForMember(dest => dest.Winner, opt => opt.Ignore())
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
            .ReverseMap()
            .ForMember(dest => dest.Guild, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Itens, opt => opt.Ignore())
            .ForMember(dest => dest.Party, opt => opt.Ignore())
            .ForMember(dest => dest.Activities, opt => opt.Ignore())
            .ForMember(dest => dest.Applications, opt => opt.Ignore());


        CreateMap<Character, CharacterSummaryDto>()
            .ReverseMap();

        CreateMap<Guild, GuildDto>()
            .ReverseMap()
            .ForMember(dest => dest.GuildMaster, opt => opt.Ignore())
            .ForMember(dest => dest.Staff, opt => opt.Ignore())
            .ForMember(dest => dest.Members, opt => opt.Ignore())
            .ForMember(dest => dest.Auctions, opt => opt.Ignore())
            .ForMember(dest => dest.Parties, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.Activities, opt => opt.Ignore())
            .ForMember(dest => dest.Applicants, opt => opt.Ignore())
            .ForMember(dest => dest.Balances, opt => opt.Ignore());

        CreateMap<Guild, GuildSummaryDto>()
            .ReverseMap();

        CreateMap<Item, ItemDto>()
            .ReverseMap()
            .ForMember(dest => dest.Auction, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

        CreateMap<Item, ItemSummaryDto>()
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ReverseMap()
            .ForMember(dest => dest.Characters, opt => opt.Ignore())
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

        CreateMap<Party, PartySummaryDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap()
            .ForMember(dest => dest.Characters, opt => opt.Ignore())
            .ForMember(dest => dest.StaffGuilds, opt => opt.Ignore())
            .ForMember(dest => dest.OwnedGuilds, opt => opt.Ignore());

        CreateMap<User, UserSummaryDto>()
            .ReverseMap();

        CreateMap<Activity, ActivityDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Guild, opt => opt.Ignore())
            .ForMember(dest => dest.Participants, opt => opt.Ignore());

        CreateMap<Activity, ActivitySummaryDto>()
            .ReverseMap();

        CreateMap<Balance, BalanceDto>()
            .ReverseMap()
            .ForMember(dest => dest.Character, opt => opt.Ignore())
            .ForMember(dest => dest.Guild, opt => opt.Ignore());

         CreateMap<Balance, BalanceSummaryDto>();
    }
}
