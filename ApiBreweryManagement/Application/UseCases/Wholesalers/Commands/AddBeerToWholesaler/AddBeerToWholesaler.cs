using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.AddBeerToWholesaler
{
    public class AddBeerToWholesalerCommand
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
    }

    public class AddBeerToWholesalerHandler
    {
        private readonly IApplicationDbContext _context;

        public AddBeerToWholesalerHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddBeerToWholesalerCommand command)
        {
            var wholesaler = await _context.Wholesalers.FindAsync(command.WholesalerId);
            if (wholesaler == null) throw new Exception("Grossiste introuvable.");

            var beer = await _context.Beers.FindAsync(command.BeerId);
            if (beer == null) throw new Exception("Bière introuvable.");

            var existingStock = await _context.WholesalerStocks
                .FirstOrDefaultAsync(ws => ws.WholesalerId == command.WholesalerId && ws.BeerId == command.BeerId);

            if (existingStock != null) throw new Exception("Cette bière est déjà vendue par ce grossiste.");

            var newStock = new WholesalerStock
            {
                WholesalerId = command.WholesalerId,
                BeerId = command.BeerId,
                Quantity = 0
            };

            _context.WholesalerStocks.Add(newStock);
            await _context.SaveChangesAsync();
        }
    }
}
