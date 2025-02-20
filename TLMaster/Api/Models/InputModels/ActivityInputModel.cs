using System;
using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class ActivityInputModel : IInputModel<ActivityDto>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Payout { get; set; }

    public bool WasPaid { get; set; }

    public DateTime DateTime { get; set; }

    public bool IsPasswordRequired { get; set; }

    public string? Password { get; set; }

    public Guid GuildId { get; set; }

    public ActivityDto InputToDto()
    => new ()
    {
        Id = Guid.NewGuid(),
        Name = Name,
        Description = Description,
        DateTime = DateTime,
        IsPasswordRequired = IsPasswordRequired,
        Password = Password,
        GuildId = GuildId
    };

    public void InputToDto(ActivityDto dto)
    {
        dto.Name = Name;
        dto.Description = Description;
        dto.DateTime = DateTime;
        dto.IsPasswordRequired = IsPasswordRequired;
        dto.Password = Password;
        dto.GuildId = GuildId;
    }
}
