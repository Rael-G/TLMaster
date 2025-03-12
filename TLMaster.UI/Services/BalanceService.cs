using TLMaster.UI.Model.InputModels;
using TLMaster.UI.Model.Models;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class BalanceService(HttpClientProvider httpClientProvider) 
    : BaseService<BalanceModel, BalanceInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/balances";
}
