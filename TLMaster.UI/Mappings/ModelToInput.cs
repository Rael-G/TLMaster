using System;
using AutoMapper;
using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;

namespace TLMaster.UI.Mappings;

public class ModelToInput : Profile
{
    public ModelToInput()
    {
        CreateMap<ActivityModel, ActivityInputModel>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.GuildId));

        CreateMap<AuctionModel, AuctionInputModel>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.GuildId))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.WinnerId, opt => opt.MapFrom(src => src.WinnerId));

        CreateMap<BidModel, BidInputModel>()
            .ForMember(dest => dest.BidderId, opt => opt.MapFrom(src => src.Bidder.Id))
            .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.Auction.Id));

        CreateMap<CharacterModel, CharacterInputModel>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.GuildId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.ApplicationIds, opt => opt.MapFrom(src => src.Applications.Select(app => app.Id)));

        CreateMap<GuildModel, GuildInputModel>()
            .ForMember(dest => dest.GuildMasterId, opt => opt.MapFrom(src => src.GuildMaster.Id))
            .ForMember(dest => dest.StaffIds, opt => opt.MapFrom(src => src.Staff.Select(staff => staff.Id)));

        CreateMap<ItemModel, ItemInputModel>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.GuildId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

        CreateMap<PartyModel, PartyInputModel>()
            .ForMember(dest => dest.GuildId, opt => opt.MapFrom(src => src.GuildId))
            .ForMember(dest => dest.CharacterIds, opt => opt.MapFrom(src => src.Characters.Select(character => character.Id)));
    }
}