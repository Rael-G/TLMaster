namespace TLMaster.UI.Dtos;

public class PartyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public Guid GuildId { get; set; }
}
