using TLMaster.UI.Models.Dtos.Summaries;
using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.Dtos;

public class CharacterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public GuildSummaryDto? Guild { get; set; }
    public List<ItemSummaryDto> Items { get; set; } = [];
    public int Coin { get; set; }
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public UserSummaryDto User { get; set; } = new();
    public List<ActivitySummaryDto> Activities { get; set; } = [];
}
