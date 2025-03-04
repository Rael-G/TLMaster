using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using TLMaster.UI.Models.Dtos;

namespace TLMaster.UI.Providers;

public class UserProvider(HttpClientProvider httpClientProvider, TokenProvider tokenProvider)
{
    private readonly HttpClient _httpClient = httpClientProvider.GetAuthenticatedClient();
    private readonly TokenProvider _tokenProvider = tokenProvider;

    public async Task<UserDto?> GetUser() => await (
        await _httpClient.GetAsync($"api/user/{await GetUserId()}"))
        .Content.ReadFromJsonAsync<UserDto>();

    public async Task<string?> GetUserId()
    {
        var token = await _tokenProvider.GetAccessToken();
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        return jwtSecurityToken?.Claims?.FirstOrDefault(c => c.Type == "nameid")?.Value;
    }
}
