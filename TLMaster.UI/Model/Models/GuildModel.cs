namespace TLMaster.UI.Model.Models;

public class GuildModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserModel GuildMaster { get; set; } = new();
    public List<UserModel> Staff { get; set; } = [];
    public List<CharacterModel> Characters { get; set; } = [];
    public List<AuctionModel> Auctions { get; set; } = [];
    public List<PartyModel> Parties { get; set; } = [];
    public List<ItemModel> Items { get; set; } = [];
    public List<ActivityModel> Activities { get; set; } = [];
}
