namespace TLMaster.UI.Model.Models;

public class UserModel
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public List<CharacterModel> Characters { get; set; } = [];
    public List<GuildModel> OwnedGuilds { get; set; } = [];
    public List<GuildModel> StaffGuilds { get; set; } = [];
}
