using System;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Interfaces;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<TDto>(IBaseService<TDto> service)
    : ControllerBase
    where TDto : IDto
{
    protected readonly IBaseService<TDto> Service = service;

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>Returns a list of all entities.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    protected async Task<IActionResult> GetAll()
        => Ok(await Service.GetAll());

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
        var entity = await Service.GetById(id);

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
            await Service.Create(entity);
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

        var entity = await Service.GetById(id);
        if (entity is null)
            return NotFound(new {Id = id});

        input.InputToDto(entity);
        try
        {
            await Service.Update(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
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
        var entity = await Service.GetById(id);

        if (entity is null)
            return NotFound(new {Id = id});

        await Service.Delete(entity);

        return NoContent();
    }
}
