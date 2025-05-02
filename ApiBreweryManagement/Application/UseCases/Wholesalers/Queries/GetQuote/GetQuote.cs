using ApiBreweryManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBreweryManagement.Application.UseCases.Wholesalers.Queries.GetQuote
{
    public class GetQuoteQuery
    {
        public int WholesalerId { get; set; }
        public List<(int BeerId, int Quantity)> Order { get; set; } = new();
    }

    public class GetQuoteHandler
    {
        private readonly IApplicationDbContext _context;

        public GetQuoteHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(GetQuoteQuery query)
        {
            if (query.Order == null || !query.Order.Any())
                throw new ArgumentException("La commande ne peut pas être vide.");

            if (query.Order.GroupBy(o => o.BeerId).Any(g => g.Count() > 1))
                throw new ArgumentException("La commande ne peut pas contenir de doublons.");

            var wholesaler = await _context.Wholesalers
                .Include(w => w.WholesalerStocks)
                .FirstOrDefaultAsync(w => w.Id == query.WholesalerId);

            if (wholesaler == null)
                throw new ArgumentException("Le grossiste spécifié n'existe pas.");

            decimal totalPrice = 0;
            int totalQuantity = 0;

            foreach (var orderItem in query.Order)
            {
                var stock = wholesaler.WholesalerStocks.FirstOrDefault(ws => ws.BeerId == orderItem.BeerId);

                if (stock == null)
                    throw new ArgumentException($"La bière avec l'ID {orderItem.BeerId} n'est pas vendue par ce grossiste.");

                if (orderItem.Quantity > stock.Quantity)
                    throw new ArgumentException($"La quantité commandée pour la bière avec l'ID {orderItem.BeerId} dépasse le stock disponible.");

                totalPrice += stock.Beer.Price * orderItem.Quantity;
                totalQuantity += orderItem.Quantity;
            }

            if (totalQuantity > 20)
                totalPrice *= 0.8m;
            else if (totalQuantity > 10)
                totalPrice *= 0.9m;

            return totalPrice;
        }
    }
}
