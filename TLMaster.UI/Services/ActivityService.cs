using TLMaster.UI.Models.Dtos;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class ActivityService(HttpClientProvider httpClientProvider) : BaseService<ActivityDto, ActivityInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/guild";
}
