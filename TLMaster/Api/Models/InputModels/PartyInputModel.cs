namespace TLMaster.Api.Models.InputModels;

public class PartyInputModel
{
    public string Name { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public Guid GuildId { get; set; }
}
