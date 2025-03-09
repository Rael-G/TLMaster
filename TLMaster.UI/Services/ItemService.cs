using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class ItemService(HttpClientProvider httpClientProvider) : BaseService<ItemModel, ItemInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/items";
}
