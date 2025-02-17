using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController(IPartyService service) : BaseController<PartyDto>(service)
    {
    }
}
