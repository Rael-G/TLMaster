using TLMaster.UI.Models.Dtos;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class AuctionService(HttpClientProvider httpClientProvider) : BaseService<AuctionDto, AuctionInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/auctions";
}
