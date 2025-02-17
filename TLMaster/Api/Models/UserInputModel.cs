using System;
using TLMaster.Api.Interfaces;
using TLMaster.Application;

namespace TLMaster.Api.Models;

public class UserInputModel : IInputModel<UserDto>
{
    public string Password { get; set; } = string.Empty;

    public UserDto InputToDto()
    {
        throw new NotImplementedException();
    }

    public void InputToDto(UserDto dto)
    {
        throw new NotImplementedException();
    }
}
