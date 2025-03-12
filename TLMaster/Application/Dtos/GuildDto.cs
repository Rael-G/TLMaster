using System;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class GuildDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserSummaryDto GuildMaster { get; set; } = new();
    public Guid GuildMasterId { get; set; }
    public List<UserSummaryDto> Staff { get; set; } = [];
    public List<CharacterSummaryDto> Members { get; set; } = [];
    public List<AuctionSummaryDto> Auctions { get; set; } = [];
    public List<PartySummaryDto> Parties { get; set; } = [];
    public List<ItemSummaryDto> Items { get; set; } = [];
    public List<ActivitySummaryDto> Activities { get; set; } = [];
    public List<CharacterSummaryDto> Applicants { get; set; } = [];
}
