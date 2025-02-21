using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Models.InputModels;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityController(IActivityService service) : BaseController<ActivityDto>(service)
{
    /// <summary>
    /// Retrieves all activities.
    /// </summary>
    /// <returns>Returns a list containing all activities.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public new async Task<IActionResult> GetAll()
        => await base.GetAll();

    /// <summary>
    /// Retrieves a specific activity by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity to retrieve.</param>
    /// <returns>Returns the activity if found, otherwise returns a 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public new async Task<IActionResult> Get(Guid id)
        => await base.Get(id);

    /// <summary>
    /// Creates a new activity.
    /// </summary>
    /// <param name="input">The input model containing data for the new activity.</param>
    /// <returns>Returns the newly created activity.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] ActivityInputModel input)
        => await base.Post(input);

    /// <summary>
    /// Updates an existing activity.
    /// </summary>
    /// <param name="id">The ID of the activity to update.</param>
    /// <param name="input">The input model containing updated data for the activity.</param>
    /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] ActivityInputModel input)
        => await base.Put(id, input);

    /// <summary>
    /// Deletes an activity by its ID.
    /// </summary>
    /// <param name="id">The ID of the activity to delete.</param>
    /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public new async Task<IActionResult> Delete(Guid id)
        => await base.Delete(id);
}
