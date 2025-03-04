using System.Net.Http.Json;
using TLMaster.UI.Models.Dtos;

namespace TLMaster.UI.Providers;

public class TokenProvider(HttpClientProvider httpProvider)
{
    private readonly HttpClientProvider _httpProvider = httpProvider;

    public async Task<string?> GetAccessToken()
    {
        var client = _httpProvider.GetCredentialsClient();
        var result = await client.GetAsync("api/auth/get-token");
        if (result.IsSuccessStatusCode)
        {
            return (await result.Content.ReadFromJsonAsync<TokenDto>())?.AccessToken;
        }

        return null;
    }

    public async Task<string?> GetRefreshToken()
    {
        var client = _httpProvider.GetCredentialsClient();
        var result = await client.GetAsync("api/auth/get-token");
        if (result.IsSuccessStatusCode)
        {
            return (await result.Content.ReadFromJsonAsync<TokenDto>())?.RefreshToken;
        }

        return null;
    }
}
