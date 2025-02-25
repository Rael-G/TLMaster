using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Interfaces;
using TLMaster.Application.Interfaces;
using TLMaster.Core.Entities;

namespace TLMaster.Api.Controllers;

[ApiController]
public abstract class BaseController<TDto>(IBaseService<TDto> service)
    : ControllerBase
    where TDto : IDto
{
    protected readonly IBaseService<TDto> Service = service;

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>Returns a list containing all entities.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    protected async Task<IActionResult> GetAll()
        => Ok(await Service.GetAll(GetUserId(User)));

    /// <summary>
    /// Retrieves a specific entity post by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>Returns the entity if found, otherwise returns a 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    protected async Task<IActionResult> Get(Guid id)
    {
        var entity = await Service.GetById(id, GetUserId(User));

        if (entity is null)
            return NotFound(new {Id = id});

        return Ok(entity);
    }

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="input">The input model containing data for the new entity.</param>
    /// <returns>Returns the newly created entity.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    protected async Task<IActionResult> Post([FromBody] IInputModel<TDto> input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = input.InputToDto();
        try
        {
            await Service.Create(entity, GetUserId(User));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }

        return CreatedAtAction(nameof(Get), new { entity.Id }, entity);
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="id">The ID of the entity to update.</param>
    /// <param name="input">The input model containing updated data for the entity.</param>
    /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    protected async Task<IActionResult> Put(Guid id, [FromBody] IInputModel<TDto> input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await Service.GetById(id, GetUserId(User));
        if (entity is null)
            return NotFound(new {Id = id});

        input.InputToDto(entity);
        try
        {
            await Service.Update(entity, GetUserId(User));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message + "\n" + ex.InnerException?.Message });
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    protected async Task<IActionResult> Delete(Guid id)
    {
        var entity = await Service.GetById(id, GetUserId(User));

        if (entity is null)
            return NotFound(new {Id = id});

        await Service.Delete(entity, GetUserId(User));

        return NoContent();
    }

    protected static Guid GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = (user.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            ?? throw new NullReferenceException("User Id from claims principal is null.");
        return Guid.Parse(userIdClaim);
    }

    protected static async Task<bool> IsAdmin(ClaimsPrincipal claim, UserManager<User> userManager)
    {
        var userIdClaim = claim.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        
        if (userIdClaim is null) return false;

        var user = await userManager.FindByIdAsync(userIdClaim);
        
        if (user == null) return false;

        return await userManager.IsInRoleAsync(user,  "admin");
    }
}
