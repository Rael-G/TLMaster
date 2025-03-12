using System;

namespace TLMaster.Api.Models.InputModels;

public class BalanceInputModel
{
    public Guid Id { get; set; }
    public Guid GuildId { get; set; }
    public Guid CharacterId { get; set; }
    public int Amount { get; set; }
}
