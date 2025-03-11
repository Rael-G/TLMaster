using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Application.Dtos;
using TLMaster.Application.Enums;
using TLMaster.Application.Exceptions;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll(GetUserId(User)));
            }
            catch (ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }
        }

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
            UserDto? user;
            try
            {
                user = await _userService.GetById(id, GetUserId(User));
            }
            catch (ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }

            if (user is null)
                return NotFound(new {Id = id});

            return Ok(user);
        }

        /// <summary>
        /// Retrieves a specific user by its username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>Returns the user if found, otherwise returns a 404 Not Found.</returns>
        [HttpGet("username")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUsername([FromQuery] string username)
        {
            UserDto? user;
            try
            {
                user = await _userService.GetByUsername(username, GetUserId(User));
            }
            catch (ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }

            if (user is null)
                return NotFound(new {Username = username});

            return Ok(user);
        }

        [HttpGet("roles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoles(Guid id)
        {
            var user = await _userService.GetById(id, GetUserId(User));

            if (user is null)
                return NotFound(new {Id = id});

            IEnumerable<string>? roles;

            try
            {
                roles = await _userService.GetRoles(id, GetUserId(User));
            }
            catch (ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }
            

            return Ok(roles);
        }

        [HttpPost("roles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> UpdateRoles(Guid id, string[] roles)
        {
            var user = await _userService.GetById(id, GetUserId(User));

            if (user is null)
                return NotFound(new {Id = id});

            try
            {
                await _userService.UpdateRoles(id, roles, GetUserId(User));
            }
            catch(ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }

            return NoContent();
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
            var user = await _userService.GetById(id, GetUserId(User));

            if (user is null)
                return NotFound(new {Id = id});

            try
            {
                await _userService.Delete(user, GetUserId(User));
            }
            catch(ForbiddenAccessException e)
            {
                return Forbid(e.Message);
            }

            return NoContent();
        }

        [HttpGet("id")]
        public IActionResult GetCurrentUserId()
        {
            return Ok(GetUserId(User));
        }

        protected static Guid GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = (user.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new NullReferenceException("User Id from claims principal is null.");
            return Guid.Parse(userIdClaim);
        }
    }
}
