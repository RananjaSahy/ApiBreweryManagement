using ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.AddBeerToWholesaler;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.UpdateBeerStock;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Queries.GetQuote;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBreweryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly AddBeerToWholesalerHandler _addBeerToWholesalerHandler;
        private readonly UpdateBeerStockHandler _updateBeerStockHandler;
        private readonly GetQuoteHandler _getQuoteHandler;

        public WholesalersController(
            AddBeerToWholesalerHandler addBeerToWholesalerHandler,
            UpdateBeerStockHandler updateBeerStockHandler,
            GetQuoteHandler getQuoteHandler)
        {
            _addBeerToWholesalerHandler = addBeerToWholesalerHandler;
            _updateBeerStockHandler = updateBeerStockHandler;
            _getQuoteHandler = getQuoteHandler;
        }

        [HttpPost("add-beer")]
        public async Task<IActionResult> AddBeerToWholesaler([FromBody] AddBeerToWholesalerCommand command)
        {
            try
            {
                await _addBeerToWholesalerHandler.Handle(command);
                return Ok("Bière ajoutée au grossiste avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-stock")]
        public async Task<IActionResult> UpdateBeerStock([FromBody] UpdateBeerStockCommand command)
        {
            try
            {
                await _updateBeerStockHandler.Handle(command);
                return Ok("Stock mis à jour avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-quote")]
        public async Task<IActionResult> GetQuote([FromBody] GetQuoteQuery query)
        {
            try
            {
                var totalPrice = await _getQuoteHandler.Handle(query);
                return Ok(new { TotalPrice = totalPrice });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
