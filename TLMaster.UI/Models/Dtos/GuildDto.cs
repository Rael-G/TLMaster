using TLMaster.UI.Models.Dtos.Summaries;

namespace TLMaster.UI.Models.Dtos;

public class GuildDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserSummaryDto GuildMaster { get; set; } = new();
    public List<UserSummaryDto> Staff { get; set; } = [];
    public List<CharacterSummaryDto> Characters { get; set; } = [];
    public List<AuctionSummaryDto> Auctions { get; set; } = [];
    public List<PartySummaryDto> Parties { get; set; } = [];
    public List<ItemSummaryDto> Items { get; set; } = [];
    public List<ActivitySummaryDto> Activities { get; set; } = [];
}
