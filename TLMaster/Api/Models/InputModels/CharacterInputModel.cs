using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;
using TLMaster.Application.Dtos.Summaries;
using TLMaster.Core.Enums;

namespace TLMaster.Api.Models.InputModels;

public class CharacterInputModel : IInputModel<CharacterDto>
{
    public string Name { get; set; } = string.Empty;
    public Guid? GuildId { get; set; }
    public int Coin { get; set; }
    public Role Role { get; set; }
    public List<Weapon> Weapons { get; set; } = [];
    public Guid UserId { get; set; }
    public List<Guid> ApplicationIds { get; set; } = [];


    public CharacterDto InputToDto()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            GuildId = GuildId,
            Coin = Coin,
            Role = Role,
            Weapons = Weapons,
            UserId = UserId,
            Applications = [.. ApplicationIds.Select(id => new GuildSummaryDto() { Id = id })],
        };

    public void InputToDto(CharacterDto dto)
    {
        dto.Name = Name;
        dto.GuildId = GuildId;
        dto.Coin = Coin;
        dto.Role = Role;
        dto.Weapons = Weapons;
        dto.UserId = UserId;
        dto.Applications = [.. ApplicationIds.Select(id => new GuildSummaryDto() { Id = id })];
    }
}
