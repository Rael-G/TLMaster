namespace TLMaster.UI.Model.Models;

public class ActivityModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Payout { get; set; }
    public bool WasPaid { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsPasswordRequired { get; set; }
    public string? Password { get; set; }
    public GuildModel Guild { get; set; } = new();
    public string GuildId { get; set; } = string.Empty;
    public List<CharacterModel> Participants { get; set; } = [];
}
