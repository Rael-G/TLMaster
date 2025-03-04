using TLMaster.UI.Models.Dtos;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class BidService(HttpClientProvider httpClientProvider) : BaseService<BidDto, BidInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/guild";
}
