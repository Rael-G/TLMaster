using System;
using System.Net;
using Microsoft.JSInterop;

namespace TLMaster.UI.Interops;

public class JSHttpClient(IJSRuntime jsRuntime)
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public string BaseAddress = string.Empty;

    public async Task<HttpResponseMessage> SendAsync(
        string url, 
        HttpMethod method, 
        string? jsonBody = null,
        Dictionary<string, string>? headers = null, // new Dictionary<string, string>{{ "Content-Type", "application/json" }}, 
        CredentialsMode credentialsMode = CredentialsMode.Include
    )
    {
        var result = await _jsRuntime.InvokeAsync<JSHttpResponse>(
            "customFetch", 
            BaseAddress + url, 
            method.Method, 
            headers ?? new Dictionary<string, string>{{ "Content-Type", "application/json" }},
            jsonBody, 
            credentialsMode.ToString().ToLower()
        );

        var responseMessage = new HttpResponseMessage((HttpStatusCode)result.Status)
        {
            ReasonPhrase = result.StatusText,
            Content = new StringContent(result.Body ?? "")
        };

        foreach (var header in result.Headers)
        {
            responseMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return responseMessage;
    }

}
