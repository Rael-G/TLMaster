namespace TLMaster.UI.Models.InputModels;

public class ActivityInputModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Payout { get; set; }

    public bool WasPaid { get; set; }

    public DateTime DateTime { get; set; }

    public bool IsPasswordRequired { get; set; }

    public string? Password { get; set; }

    public Guid GuildId { get; set; }

}
