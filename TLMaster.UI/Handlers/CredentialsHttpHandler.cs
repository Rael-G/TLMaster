using System;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TLMaster.UI.Handlers;

public class CredentialsHttpHandler : DelegatingHandler
{

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        request.Headers.Add("X-Requested-With", [ "XMLHttpRequest" ]);

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}

