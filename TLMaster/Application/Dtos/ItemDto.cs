using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class ItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
}
