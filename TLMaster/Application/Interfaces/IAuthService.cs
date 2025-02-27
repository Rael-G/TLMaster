using TLMaster.Application.Dtos;

namespace TLMaster.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> GetToken();

    Task<TokenDto> RegenToken(string refreshToken);
}
