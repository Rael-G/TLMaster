using System.Net.Http.Json;
using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class ActivityService(HttpClientProvider httpClientProvider) : BaseService<ActivityModel, ActivityInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/activities";

    public async Task<bool> Participate(string activityId, string characterId, string password)
    {
        var response = await HttpClient.PutAsJsonAsync($"{Endpoint}/{activityId}/participate?characterId={characterId}", password);
        return response.IsSuccessStatusCode;
    }
}
