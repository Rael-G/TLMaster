namespace TLMaster.UI.Models.InputModels;

public class GuildInputModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string GuildMasterId { get; set; } = string.Empty;
    public List<Guid> StaffIds { get; set; } = [];
}
