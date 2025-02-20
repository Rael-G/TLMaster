using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class UserDto : IDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<Guid> CharacterIds { get; set; } = [];
    public List<Guid> OwnedGuildIds { get; set; } = [];
    public List<Guid> StaffGuildIds { get; set; } = [];
}
