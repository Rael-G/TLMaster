namespace TLMaster.Api.Models.InputModels;

public class GuildInputModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid GuildMasterId { get; set; }
    public List<Guid> StaffIds { get; set; } = new();
}
