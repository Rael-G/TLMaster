using System.Net.Http.Json;
using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class GuildService(HttpClientProvider httpClientProvider) : BaseService<GuildModel, GuildInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/guilds";

    public async Task<bool> AcceptMember(string id, string applicantId)
    {
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}/accept-member", applicantId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RejectMember(string id, string applicantId)
    {
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}/reject-member", applicantId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveMember(string id, string memberId)
    {
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{id}/remove-member", memberId);
        return response.IsSuccessStatusCode;
    }
}
