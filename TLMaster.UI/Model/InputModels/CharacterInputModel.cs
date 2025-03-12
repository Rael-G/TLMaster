using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.InputModels;

public class CharacterInputModel
{
    public string Name { get; set; } = string.Empty;
    public string? GuildId { get; set; }
    public int Coin { get; set; }
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public string UserId { get; set; } = string.Empty;
    public List<string> ApplicationIds { get; set; } = [];

}
