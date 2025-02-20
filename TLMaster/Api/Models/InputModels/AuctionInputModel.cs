using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;
using TLMaster.Core.Enums;

namespace TLMaster.Api.Models.InputModels;

public class AuctionInputModel : IInputModel<AuctionDto>
{
    public Guid ItemId { get; set; }
    public int InitialPrice { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid? WinnerId { get; set; }
    public AuctionStatus Status { get; set; }
    public Guid GuildId { get; set; }

    public AuctionDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            ItemId = ItemId,
            InitialPrice = InitialPrice,
            StartTime = StartTime,
            Duration = Duration,
            WinnerId = WinnerId,
            Status = Status,
            GuildId = GuildId
        };
    

    public void InputToDto(AuctionDto dto)
    {
        dto.ItemId = ItemId;
        dto.InitialPrice = InitialPrice;
        dto.StartTime = StartTime;
        dto.Duration = Duration;
        dto.WinnerId = WinnerId;
        dto.Status = Status;
        dto.GuildId = GuildId;
    }
}   
