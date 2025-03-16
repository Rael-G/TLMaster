namespace TLMaster.UI.Models.InputModels;

public class ActivityInputModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Payout { get; set; }

    public DateTime DateTime { get; set; }

    public string Password { get; set; } = string.Empty;

    public string GuildId { get; set; } = string.Empty;

}
