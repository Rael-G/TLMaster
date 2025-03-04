using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class ItemInputModel : IInputModel<ItemDto>
{
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
    public Guid GuildId { get; set;}

    public ItemDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Owner = OwnerId != null ? new () { Id = (Guid)OwnerId } : null,
            Guild = new () { Id = GuildId }
        };

    public void InputToDto(ItemDto dto)
    {
        dto.Name = Name;
        dto.Owner = OwnerId != null ? new () { Id = (Guid)OwnerId } : null;
        dto.Guild = new () { Id = GuildId };
    }
}
