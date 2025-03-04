using System;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class PartyService(HttpClientProvider httpClientProvider) : BaseService<PartyService, PartyInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/parties";
}