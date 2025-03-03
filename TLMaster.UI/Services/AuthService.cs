using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class AuthService(IConfiguration configuration, NavigationManager navigationManager, 
    HttpClientProvider httpClientProvider, TokenProvider tokenProvider)
{
    private readonly HttpClientProvider _httpClientProvider = httpClientProvider;
    private readonly IConfiguration _configuraton = configuration;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly TokenProvider _tokenProvider = tokenProvider;

    public void Login()
    {
        var returnUrl = Uri.EscapeDataString(_navigationManager.BaseUri + "auth/external-login-callback");
        var loginUrl = $"{_configuraton["ApiUrl"]}api/auth/login?returnUrl={returnUrl}";
        _navigationManager.NavigateTo(loginUrl);
    }

    public async Task HandleExternalLoginCallbackAsync()
    {
        var client = _httpClientProvider.GetCredentialsClient();
        var result = await client.PostAsJsonAsync("api/auth/generate-token", new {});

        if (result.IsSuccessStatusCode)
        {
            //_authStateProvider.NotifyAuthenticationStateChanged();
            _navigationManager.NavigateTo("/");
        }
        else
        {
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }
    }

    public async Task<bool> RefreshTokenAsync()
    {
        var client = _httpClientProvider.GetCredentialsClient();
        var refreshToken = await _tokenProvider.GetRefreshToken();
        var result = await client.PostAsJsonAsync("api/auth/regen-token", new { refreshToken });

        if (result.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            await Logout();
            return false;
        }
    }

    public async Task Logout()
    {
        var client = _httpClientProvider.GetCredentialsClient();
        await client.PostAsJsonAsync("api/auth/logout", new { });
        //_authStateProvider.NotifyAuthenticationStateChanged();
    }

}