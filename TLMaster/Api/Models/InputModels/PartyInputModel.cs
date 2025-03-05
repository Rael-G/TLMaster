using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;
using TLMaster.Application.Dtos.Summaries;

namespace TLMaster.Api.Models.InputModels;

public class PartyInputModel : IInputModel<PartyDto>
{
    public string Name { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public Guid GuildId { get; set; }

     public PartyDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Characters = [.. CharacterIds.Select(id => new CharacterSummaryDto() { Id = id })],
            GuildId = GuildId
        };

    public void InputToDto(PartyDto dto)
    {
        dto.Name = Name;
        dto.Characters = [.. CharacterIds.Select(id => new CharacterSummaryDto() { Id = id })];
        dto.GuildId = GuildId;
    }
}
