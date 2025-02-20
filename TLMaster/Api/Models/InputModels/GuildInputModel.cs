using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class GuildInputModel : IInputModel<GuildDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid GuildMasterId { get; set; }
    public List<Guid> StaffIds { get; set; } = [];

    public GuildDto InputToDto()
        => new () 
        { 
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description,
            GuildMasterId = GuildMasterId,
            StaffIds = StaffIds
        };

    public void InputToDto(GuildDto dto)
    {
        dto.Name = Name;
        dto.Description = Description;
        dto.GuildMasterId = GuildMasterId;
        dto.StaffIds = StaffIds;
    }
}
