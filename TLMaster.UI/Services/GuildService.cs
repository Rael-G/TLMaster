using System.Net.Http.Json;
using TLMaster.UI.Models.Dtos;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class GuildService
{
    private readonly HttpClientProvider _httpClientProvider;
    private readonly HttpClient _httpClient;
    private const string Endpoint = "api/guild";

    public GuildService(HttpClientProvider httpClientProvider)
    {
        _httpClientProvider = httpClientProvider;
        _httpClient = _httpClientProvider.GetAuthenticatedClient();
    }

    public async Task<List<GuildDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GuildDto>>(Endpoint) 
            ?? [];
    }

    public async Task<GuildDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<GuildDto>($"{Endpoint}/{id}");
    }

    public async Task<bool> CreateAsync(GuildInputModel input)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, input);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(Guid id, GuildInputModel input)
    {
        var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", input);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}
