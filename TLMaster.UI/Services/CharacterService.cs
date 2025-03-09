using TLMaster.UI.Model.Models;
using TLMaster.UI.Models.InputModels;
using TLMaster.UI.Providers;

namespace TLMaster.UI.Services;

public class CharacterService(HttpClientProvider httpClientProvider) : BaseService<CharacterModel, CharacterInputModel>(httpClientProvider)
{
    protected override string Endpoint { get; set; } = "api/characters";
}