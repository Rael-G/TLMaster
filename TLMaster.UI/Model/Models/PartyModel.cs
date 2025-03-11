namespace TLMaster.UI.Model.Models;

public class PartyModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<CharacterModel> Characters { get; set; } = [];
    public GuildModel Guild { get; set; } = new();
    public string GuildId { get; set; } = string.Empty;
}
