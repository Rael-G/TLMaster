using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using TLMaster.UI.Dtos;
using TLMaster.UI.Interops;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class AuthService(ILocalStorageService localStorage, JSHttpClient jsHttpClient, IConfiguration configuration, NavigationManager navigationManager, ApplicationAuthStateProvider authStateProvider)
{
    private readonly JSHttpClient _jsHttpClient = jsHttpClient;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly IConfiguration _configuraton = configuration;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ApplicationAuthStateProvider _authStateProvider = authStateProvider;

    public void Login()
    {
        var returnUrl = Uri.EscapeDataString(_navigationManager.BaseUri + "auth/external-login-callback");
        var loginUrl = $"{_configuraton["ApiUrl"]}api/auth/login?returnUrl={returnUrl}";
        _navigationManager.NavigateTo(loginUrl);
    }

    public async Task HandleExternalLoginCallbackAsync()
    {
        // Blazor documentation about how to include cookies on request
        // Does not work
        // var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/auth/get-token");
        // requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        // requestMessage.Headers.Add("X-Requested-With", [ "XMLHttpRequest" ]);
        // var result = await _httpClient.SendAsync(requestMessage);

        var result = await _jsHttpClient.SendAsync("api/auth/get-token", HttpMethod.Get);

        if (result.IsSuccessStatusCode)
        {
            var token = await result.Content.ReadFromJsonAsync<TokenDto>();

            if (!string.IsNullOrEmpty(token?.AccessToken) && !string.IsNullOrEmpty(token?.RefreshToken))
            {
                await StoreTokensAsync(token.AccessToken, token.RefreshToken);
                _authStateProvider.NotifyAuthenticationStateChanged();
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _navigationManager.NavigateTo("/login-failed");
            }
        }
        else
        {
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }
    }

    public async Task Logout()
    {
        await ClearTokensAsync();
        _authStateProvider.NotifyAuthenticationStateChanged();
    }

    public async Task<string?> GetAccessToken()
    {
        return await _localStorage.GetItemAsync<string>("accessToken");
    }

    private async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemsAsync(["accessToken", "refreshToken"]);
    }

    private async Task StoreTokensAsync(string token, string refreshToken)
    {
        await _localStorage.SetItemAsync("accessToken", token);
        await _localStorage.SetItemAsync("refreshToken", refreshToken);
    }
}