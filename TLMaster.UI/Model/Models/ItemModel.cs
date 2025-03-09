namespace TLMaster.UI.Model.Models;

public class ItemModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public CharacterModel? Owner { get; set; }
    public GuildModel Guild { get; set; } = new();
    public string GuildId { get; set; } = string.Empty;
}
