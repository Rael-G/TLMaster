using Microsoft.AspNetCore.Mvc;
using TLMaster.Api.Models.InputModels;
using TLMaster.Application.Dtos;
using TLMaster.Application.Interfaces;

namespace TLMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController(IAuctionService service) : BaseController<AuctionDto>(service)
    {
        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>Returns a list containing all auctions.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public new async Task<IActionResult> GetAll()
            => await base.GetAll();

        /// <summary>
        /// Retrieves a specific auction by its ID.
        /// </summary>
        /// <param name="id">The ID of the auction to retrieve.</param>
        /// <returns>Returns the auction if found, otherwise returns a 404 Not Found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public new async Task<IActionResult> Get(Guid id)
            => await base.Get(id);

        /// <summary>
        /// Creates a new auction.
        /// </summary>
        /// <param name="input">The input model containing data for the new auction.</param>
        /// <returns>Returns the newly created auction.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AuctionInputModel input)
            => await base.Post(input);

        /// <summary>
        /// Updates an existing auction.
        /// </summary>
        /// <param name="id">The ID of the auction to update.</param>
        /// <param name="input">The input model containing updated data for the auction.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found or 400 Bad Request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] AuctionInputModel input)
            => await base.Put(id, input);

        /// <summary>
        /// Deletes an auction by its ID.
        /// </summary>
        /// <param name="id">The ID of the auction to delete.</param>
        /// <returns>Returns 204 No Content if successful, otherwise returns a 404 Not Found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public new async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}
