using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace TLMaster.UI.Providers;

public class HttpClientProvider(HttpClient httpClient, ILocalStorageService localStorage)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task<HttpClient> GetClient() 
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return _httpClient;
    }

}
