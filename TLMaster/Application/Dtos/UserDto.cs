using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class UserDto : IDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<CharacterSummaryDto> Characters { get; set; } = [];
    public List<GuildSummaryDto> OwnedGuilds { get; set; } = [];
    public List<GuildSummaryDto> StaffGuilds { get; set; } = [];
}
