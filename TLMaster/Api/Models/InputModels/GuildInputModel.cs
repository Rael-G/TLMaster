using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class GuildInputModel : IInputModel<GuildDto>
{
    public Guid GuildMasterId { get; set; }
    public List<Guid> StaffIds { get; set; } = [];

    public GuildDto InputToDto()
        => new () 
        { 
            Id = Guid.NewGuid(),
            GuildMasterId = GuildMasterId,
            StaffIds = StaffIds
        };

    public void InputToDto(GuildDto dto)
    {
        dto.GuildMasterId = GuildMasterId;
        dto.StaffIds = StaffIds;
    }
}
