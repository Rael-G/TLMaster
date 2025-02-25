using System;
using System.Net.Http.Headers;
using TLMaster.UI.Services;

namespace TLMaster.UI.Providers;

public class HttpClientProvider(HttpClient httpClient, AuthService authService)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AuthService _authService = authService;


    public async Task<HttpClient> GetClient() 
    {
        var token = await _authService.GetAccessToken();
        
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return _httpClient;
    }

}
