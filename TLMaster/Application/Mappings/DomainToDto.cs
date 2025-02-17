using System;
using AutoMapper;
using TLMaster.Core.Entities;

namespace TLMaster.Application.Mappings;

public class DomainToDto : Profile
{
    public DomainToDto()
    {
        CreateMap<Auction, AuctionDto>()
            .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids))
            .ForMember(dest => dest.Winner, opt => opt.MapFrom(src => src.Winner))
            .ReverseMap();

        CreateMap<Bid, BidDto>()
            .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.Auction.Id)) // Evita loop
            .ReverseMap();

        CreateMap<Character, CharacterDto>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.Guild != null ? src.Guild.Id : (Guid?)null))
            .ReverseMap();

        CreateMap<Guild, GuildDto>()
            .ForMember(dest => dest.GuildMasterId, opt => opt.MapFrom(src => src.GuildMaster.Id))
            .ForMember(dest => dest.StaffIds, opt => opt.MapFrom(src => src.Staff.Select(u => u.Id)))
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ForMember(dest => dest.AuctionIds, opt => opt.MapFrom(src => src.Auctions.Select(a => a.Id)))
            .ForMember(dest => dest.PartyIds, opt => opt.MapFrom(src => src.Parties.Select(p => p.Id)))
            .ReverseMap();

        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.Id : (Guid?)null))
            .ReverseMap();

        CreateMap<Party, PartyDto>()
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(c => c.Id)))
            .ReverseMap();
    }
}
