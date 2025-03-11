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
        var userId = await GetUserId()?? string.Empty;
        var result =  await _httpClient.GetAsync($"api/users/{userId}");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<UserModel>();
        }
        else
        {
            return null;
        }
    }
        

    public async Task<string?> GetUserId()
    {
        var result = await _httpClient.GetAsync("api/users/id");

        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<string>();
        }
        else
        {
            return null;
        }
    }
}
