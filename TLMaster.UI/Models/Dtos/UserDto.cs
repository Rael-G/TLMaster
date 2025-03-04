using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<CharacterSummaryDto> Characters { get; set; } = [];
    public List<GuildSummaryDto> OwnedGuilds { get; set; } = [];
    public List<GuildSummaryDto> StaffGuilds { get; set; } = [];
}
