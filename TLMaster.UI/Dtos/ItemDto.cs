namespace TLMaster.UI.Dtos;

public class ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
    public Guid GuildId { get; set; }
}
