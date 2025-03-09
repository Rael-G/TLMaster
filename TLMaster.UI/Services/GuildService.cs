using System.Net.Http.Json;
using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class GuildService(HttpClientProvider httpClientProvider) : BaseService<GuildModel, GuildInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/guilds";
}
