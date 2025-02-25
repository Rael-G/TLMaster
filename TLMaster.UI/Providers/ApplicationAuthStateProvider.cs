using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;

namespace TLMaster.UI.Providers;

public class ApplicationAuthStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage = localStorage;

    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = null;
        try
        {
            token = await GetAccessToken();
        }
        catch(InvalidOperationException)
        {
            // Server side
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        _currentUser = new ClaimsPrincipal(identity);

        return new AuthenticationState(_currentUser);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private async Task<string?> GetAccessToken() 
        => await _localStorage.GetItemAsync<string>("accessToken");

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
