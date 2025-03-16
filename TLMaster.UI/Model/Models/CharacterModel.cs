using TLMaster.UI.Models.Enums;

namespace TLMaster.UI.Model.Models;

public class CharacterModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public GuildModel? Guild { get; set; }
    public string? GuildId { get; set; }
    public List<ItemModel> Items { get; set; } = [];
    public BalanceModel? Balance { get; set; } = new();
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public UserModel User { get; set; } = new();
    public string UserId { get; set; } = string.Empty;
    public string? PartyId { get; set; }
    public List<ActivityModel> Activities { get; set; } = [];
    public List<GuildModel> Applications { get; set; } = [];
}
