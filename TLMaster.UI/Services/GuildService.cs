using System.Net.Http.Json;
using TLMaster.UI.Models.Dtos;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class GuildService(HttpClientProvider httpClientProvider) : BaseService<GuildDto, GuildInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/guilds";
}
