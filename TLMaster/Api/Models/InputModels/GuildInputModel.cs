using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;
using TLMaster.Application.Dtos.Summaries;

namespace TLMaster.Api.Models.InputModels;

public class GuildInputModel : IInputModel<GuildDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid GuildMasterId { get; set; }
    public List<Guid> StaffIds { get; set; } = new();

    public GuildDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description,
            GuildMaster = new() { Id = GuildMasterId },
            Staff = [.. StaffIds.Select(id => new UserSummaryDto () { Id = id })]
        };

    public void InputToDto(GuildDto dto)
    {
        dto.Name = Name;
        dto.Description = Description;
        dto.GuildMaster = new() { Id = GuildMasterId };
        dto.Staff = [.. StaffIds.Select(id => new UserSummaryDto () { Id = id })];
    }
}
