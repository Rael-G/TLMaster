using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuildController(IGuildService service) : BaseController<GuildDto>(service)
    {
    }
}
