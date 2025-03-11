namespace TLMaster.UI.Models.InputModels;

public class PartyInputModel
{
    public string Name { get; set; } = string.Empty;
    public List<string> CharacterIds { get; set; } = [];
    public string GuildId { get; set; } = string.Empty;
}
