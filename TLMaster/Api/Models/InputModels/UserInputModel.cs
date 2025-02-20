using TLMaster.Api.Interfaces;
using TLMaster.Application.Dtos;

namespace TLMaster.Api.Models.InputModels;

public class UserInputModel : IInputModel<UserDto>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public UserDto InputToDto()
        => new () 
        {
            UserName = UserName
        };

    public void InputToDto(UserDto dto)
    {
        dto.UserName = UserName;
    }
}
