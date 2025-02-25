using TLMaster.Application.Dtos;

namespace TLMaster.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> Login();

    Task<TokenDto> RegenToken(string refreshToken);
}
