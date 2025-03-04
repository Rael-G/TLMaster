namespace TLMaster.UI.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public List<Guid> OwnedGuildIds { get; set; } = [];
    public List<Guid> StaffGuildIds { get; set; } = [];
}
