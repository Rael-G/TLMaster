using System;
using System.Net.Http.Json;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public abstract class BaseService<TDto, TInput>(HttpClientProvider httpClientProvider)
{
    protected readonly HttpClient HttpClient = httpClientProvider.GetAuthenticatedClient();
    protected abstract string Endpoint { get; set;}

    public virtual async Task<List<TDto>> GetAllAsync()
    {
        return await HttpClient.GetFromJsonAsync<List<TDto>>(Endpoint) 
            ?? [];
    }

    public virtual async Task<TDto?> GetByIdAsync(string id)
    {
        return await HttpClient.GetFromJsonAsync<TDto>($"{Endpoint}/{id}");
    }

    public virtual async Task<bool> CreateAsync(TInput input)
    {
        var response = await HttpClient.PostAsJsonAsync(Endpoint, input);
        return response.IsSuccessStatusCode;
    }

    public virtual async Task<bool> UpdateAsync(string id, TInput input)
    {
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}", input);
        return response.IsSuccessStatusCode;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var response = await HttpClient.DeleteAsync($"{Endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}
