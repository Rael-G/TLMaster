using System.Net;
using System.Net.Http.Headers;
using TLMaster.UI.Providers;
using TLMaster.UI.Services;

namespace TLMaster.UI.Handlers;

public class AuthenticatedHttpHandler(AuthService authService, TokenProvider tokenProvider) : DelegatingHandler
{
    private readonly AuthService _authService = authService;
    private readonly TokenProvider _tokenProvider = tokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetAccessToken();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            bool refreshed = await _authService.RefreshTokenAsync();

            if (refreshed)
            {
                token = await _tokenProvider.GetAccessToken();

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}

