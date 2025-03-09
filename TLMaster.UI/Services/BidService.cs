using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class BidService(HttpClientProvider httpClientProvider) : BaseService<BidModel, BidInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/bids";
}
