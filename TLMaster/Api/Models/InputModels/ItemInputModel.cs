namespace TLMaster.Api.Models.InputModels;

public class ItemInputModel
{
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
    public Guid GuildId { get; set;}
}
