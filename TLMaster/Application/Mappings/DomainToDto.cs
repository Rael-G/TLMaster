using System;
using AutoMapper;
using TLMaster.Application.Dtos;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Mappings;

public class DomainToDto : Profile
{
    public DomainToDto()
    {
        CreateMap<Auction, AuctionDto>()
            .ReverseMap();

        CreateMap<Bid, BidDto>()
            .ReverseMap();

        CreateMap<Character, CharacterDto>()
            .ReverseMap();

        CreateMap<Guild, GuildDto>()
            .ForMember(dest => dest.StaffIds, opt => opt.MapFrom(src => src.Staff.Select(u => u.Id)))
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ForMember(dest => dest.AuctionIds, opt => opt.MapFrom(src => src.Auctions.Select(a => a.Id)))
            .ForMember(dest => dest.PartyIds, opt => opt.MapFrom(src => src.Parties.Select(p => p.Id)))
            .ReverseMap();

        CreateMap<Item, ItemDto>()
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ReverseMap();

        CreateMap<Activity, ActivityDto>()
            .ForMember(dest => dest.ParticipantIds, opt => opt.MapFrom(src => src.Participants.Select(c => c.Id)))
            .ReverseMap();
    }
}
