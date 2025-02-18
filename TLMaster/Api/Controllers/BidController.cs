using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Models.InputModels;
using TLMaster.Application;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController(IBidService service) : BaseController<BidDto>(service)
    {
        /// <summary>
        /// Retrieves all bids.
        /// </summary>
        /// <returns>Returns a list containing all bids.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public new async Task<IActionResult> GetAll()
            => await base.GetAll();

        /// <summary>
        /// Retrieves a specific bid by its ID.
        /// </summary>
        /// <param name="id">The ID of the bid to retrieve.</param>
        /// <returns>Returns the bid if found, otherwise returns a 404 Not Found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public new async Task<IActionResult> Get(Guid id)
            => await base.Get(id);

        /// <summary>
        /// Creates a new bid.
        /// </summary>
        /// <param name="input">The input model containing data for the new bid.</param>
        /// <returns>Returns the newly created bid.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BidInputModel input)
            => await base.Post(input);

        /// <summary>
        /// Updates an existing bid.
        /// </summary>
        /// <param name="id">The ID of the bid to update.</param>
        /// <param name="input">The input model containing updated data for the bid.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found or 400 Bad Request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] BidInputModel input)
            => await base.Put(id, input);

        /// <summary>
        /// Deletes a bid by its ID.
        /// </summary>
        /// <param name="id">The ID of the bid to delete.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public new async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}
