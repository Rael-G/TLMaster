using System;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace TLMaster.UI.Services;

public class AuthService(ILocalStorageService localStorage, HttpClient httpClient, IConfiguration configuration, NavigationManager navigationManager, ApplicationAuthStateProvider authStateProvider)
{
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly IConfiguration _configuraton = configuration;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly ApplicationAuthStateProvider _authStateProvider = authStateProvider;
    private readonly HttpClient _httpClient = httpClient;


    public void Login()
    {
        var returnUrl = Uri.EscapeDataString(_navigationManager.BaseUri + "auth/external-login-callback");
        var loginUrl = $"{_configuraton["ApiUrl"]}/auth/login?returnUrl={returnUrl}";
        _navigationManager.NavigateTo(loginUrl);
    }

    public async Task HandleExternalLoginCallbackAsync(string returnUrl)
    {
        var uri = _navigationManager.Uri;

        var queryParams = new Uri(uri).Query;
        var token = ExtractTokenFromQuery(queryParams, "access_token");
        var refreshToken = ExtractTokenFromQuery(queryParams, "refresh_token");

        if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken))
        {
            await StoreTokensAsync(token, refreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _authStateProvider.NotifyAuthenticationStateChanged();
            _navigationManager.NavigateTo(returnUrl);
        }
        else
        {
            _navigationManager.NavigateTo("/login-failed");
        }
    }

    public async Task Logout()
    {
        await ClearTokensAsync();
        _httpClient.DefaultRequestHeaders.Authorization = default;
        _authStateProvider.NotifyAuthenticationStateChanged();
    }

    public async Task<string?> GetAccessToken() 
        => await _localStorage.GetItemAsync<string>("accessToken");

    private string ExtractTokenFromQuery(string queryParams, string tokenName)
    {
        var uriQuery = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryParams);
        return uriQuery.TryGetValue(tokenName, out Microsoft.Extensions.Primitives.StringValues value) ? value.ToString() : string.Empty;
    }

    public async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemsAsync(["accessToken", "refreshToken"]);
    }

    private async Task StoreTokensAsync(string token, string refreshToken)
    {
        await _localStorage.SetItemAsync("accessToken", token);
        await _localStorage.SetItemAsync("refreshToken", refreshToken);
    }
}

public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}