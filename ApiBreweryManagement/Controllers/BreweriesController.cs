using ApiBreweryManagement.Application.UseCases.Beers.Commands.AddBeer;
using ApiBreweryManagement.Application.UseCases.Beers.Commands.DeleteBeer;
using ApiBreweryManagement.Application.UseCases.Beers.Queries;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Queries.GetQuote;
using ApiBreweryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiBreweryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweriesController : ControllerBase
    {
        private readonly AddBeerHandler _addBeerHandler;
        private readonly DeleteBeerHandler _deleteBeerHandler;
        private readonly GetBeersByBreweryHandler _getBeersByBreweryHandler;
        public BreweriesController(AddBeerHandler addBeerHandler, GetBeersByBreweryHandler getBeersByBreweryHandler, DeleteBeerHandler deleteBeerHandler)
        {
            _addBeerHandler = addBeerHandler;
            _deleteBeerHandler = deleteBeerHandler;
            _getBeersByBreweryHandler = getBeersByBreweryHandler;
        }

        [HttpGet("{breweryId}/beers")]
        public async Task<ActionResult<List<BeerDto>>> GetBeersByBrewery(int breweryId)
        {
            try
            {
                var beers = await _getBeersByBreweryHandler.Handle(new GetBeersByBreweryQuery { BreweryId = breweryId });
                return Ok(beers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{breweryId}/beers")]
        public async Task<IActionResult> AddBeer(int breweryId, [FromBody] AddBeerCommand command)
        {
            if (breweryId != command.BreweryId)
            {
                return BadRequest("L'ID de la brasserie dans l'URL ne correspond pas à celui de la commande.");
            }

            try
            {
                await _addBeerHandler.Handle(command);
                return CreatedAtAction(nameof(GetBeersByBrewery), new { breweryId = command.BreweryId }, command);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{breweryId}/beers/{beerId}")]
        public async Task<IActionResult> DeleteBeer(int breweryId, int beerId)
        {
            try
            {
                await _deleteBeerHandler.Handle(new DeleteBeerCommand { BreweryId = breweryId, BeerId = beerId });
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
