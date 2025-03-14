using System;

namespace TLMaster.UI.Model.Models;

public class BalanceModel
{
    public string Id { get; set; } = string.Empty;
    
    public string GuildId { get; set; } = string.Empty;

    public string CharacterId { get; set; } = string.Empty;

    public int Amount { get; set; }
}