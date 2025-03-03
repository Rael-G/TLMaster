namespace TLMaster.UI.Providers;

public class HttpClientProvider(IHttpClientFactory httpClientFactory)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public HttpClient GetCredentialsClient() 
    {
        return  _httpClientFactory.CreateClient("Credentials");
    }

    public HttpClient GetAuthenticatedClient() 
    {
        return  _httpClientFactory.CreateClient("Authenticated");
    }
}
