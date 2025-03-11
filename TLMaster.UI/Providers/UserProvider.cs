using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using TLMaster.UI.Model.Models;

namespace TLMaster.UI.Providers;

public class UserProvider(HttpClientProvider httpClientProvider, TokenProvider tokenProvider)
{
    private readonly HttpClient _httpClient = httpClientProvider.GetAuthenticatedClient();
    private readonly TokenProvider _tokenProvider = tokenProvider;

    public async Task<UserModel?> GetUser()
    {
        var userId = await GetUserId();
        if (userId != null)
        {
            var result =  await _httpClient.GetAsync($"api/users/{userId}");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<UserModel>();
            }
        }

        return null;
    }
        

    public async Task<string?> GetUserId()
    {
        var token = await _tokenProvider.GetAccessToken();
        var handler = new JwtSecurityTokenHandler();
        
        if (token == null) return null;
        
        var jwtSecurityToken = handler.ReadJwtToken(token);

        return jwtSecurityToken?.Claims?.FirstOrDefault(c => c.Type == "nameid")?.Value;
    }
}
