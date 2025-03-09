using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class ActivityService(HttpClientProvider httpClientProvider) : BaseService<ActivityModel, ActivityInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/activities";
}
