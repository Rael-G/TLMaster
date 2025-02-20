using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class PartyInputModel : IInputModel<PartyDto>
{
    public string Name { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public Guid GuildId { get; set; }

    public PartyDto InputToDto()
        => new ()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            CharacterIds = CharacterIds,
            GuildId = GuildId
        };

    public void InputToDto(PartyDto dto)
    {
        dto.Name = Name;
        dto.CharacterIds = CharacterIds;
        dto.GuildId = GuildId;
    }
}
