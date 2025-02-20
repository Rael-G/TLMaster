using System;
using TLMaster.Application.Interfaces;

namespace TLMaster.Application.Dtos;

public class ActivityDto : IDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DateTime { get; set; }

    public bool IsPasswordRequired { get; set; }

    public string? Password { get; set; }

    public List<Guid> ParticipantIds { get; set; } = [];

}
