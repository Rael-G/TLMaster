using System;
using TLMaster.Core.Enums;

namespace TLMaster.Application.Dtos.Summaries;

public class CharacterSummaryDto 
{ 
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public Guid? GuildId { get; set; }
    public Role Role { get; set; }
    public int Coin { get; set; }
    public Guid UserId { get; set; }

}

