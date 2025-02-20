using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Models.InputModels;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            => Ok(await _service.GetAll());

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
            var user = await _service.GetById(id);

            if (user is null)
                return NotFound(new {Id = id});

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="input">The input model containing data for the new user.</param>
        /// <returns>Returns the newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = input.InputToDto();
            IdentityResult result;
            try
            {
                result = await _service.Create(user, input.Password);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            if (!result.Succeeded)
                return BadRequest(new { result.Errors });

            return Ok();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="input">The input model containing updated data for the user.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found or 400 Bad Request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _service.GetById(id);
            if (user is null)
                return NotFound(new {Id = id});

            input.InputToDto(user);
            try
            {
                await _service.Update(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
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
            var user = await _service.GetById(id);

            if (user is null)
                return NotFound(new {Id = id});

            await _service.Delete(user);

            return NoContent();
        }
    }
}
