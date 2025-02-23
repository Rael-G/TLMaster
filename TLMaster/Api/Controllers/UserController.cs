using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll(GetUserId(User)));

        /// <summary>
        /// Retrieves a specific user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns the user if found, otherwise returns a 404 Not Found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetById(id, GetUserId(User));

            if (user is null)
                return NotFound(new {Id = id});

            return Ok(user);
        }

        /// <summary>
        /// Deletes an user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _service.GetById(id, GetUserId(User));

            if (user is null)
                return NotFound(new {Id = id});

            await _service.Delete(user, GetUserId(User));

            return NoContent();
        }

        protected static Guid GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = (user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value)
                ?? throw new NullReferenceException("User Id from claims principal is null.");
            return Guid.Parse(userIdClaim);
        }
    }
}
