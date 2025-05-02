using ApiBreweryManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.UpdateBeerStock
{
    public class UpdateBeerStockCommand
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int NewQuantity { get; set; }
    }

    public class UpdateBeerStockHandler
    {
        private readonly IApplicationDbContext _context;

        public UpdateBeerStockHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateBeerStockCommand command)
        {
            var stock = await _context.WholesalerStocks
                .FirstOrDefaultAsync(ws => ws.WholesalerId == command.WholesalerId && ws.BeerId == command.BeerId);

            if (stock == null) throw new Exception("La bière n'est pas vendue par ce grossiste.");

            if (command.NewQuantity < 0) throw new Exception("La quantité ne peut pas être négative.");

            stock.Quantity = command.NewQuantity;
            await _context.SaveChangesAsync();
        }
    }

}
