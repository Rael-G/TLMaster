using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class ItemInputModel : IInputModel<ItemDto>
{
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }

    public ItemDto InputToDto()
        => new ()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            OwnerId = OwnerId,
        };

    public void InputToDto(ItemDto dto)
    {
        dto.Name = Name;
        dto.OwnerId = OwnerId;
    }
}
