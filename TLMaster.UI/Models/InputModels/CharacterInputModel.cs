using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Models.InputModels;

public class CharacterInputModel
{
    public string Name { get; set; } = string.Empty;
    public Guid? GuildId { get; set; }
    public int Coin { get; set; }
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public Guid UserId { get; set; }
}
