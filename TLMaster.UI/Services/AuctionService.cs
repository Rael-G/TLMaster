using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class AuctionService(HttpClientProvider httpClientProvider) : BaseService<AuctionModel, AuctionInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/auctions";
}
