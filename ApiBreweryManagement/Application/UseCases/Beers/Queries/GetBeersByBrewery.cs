using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiBreweryManagement.Application.UseCases.Beers.Queries
{
    public class GetBeersByBreweryQuery
    {
        public int BreweryId { get; set; }
    }

    public class GetBeersByBreweryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetBeersByBreweryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Beer>> Handle(GetBeersByBreweryQuery query)
        {
            var brewery = await _context.Breweries.FindAsync(query.BreweryId);
            if (brewery == null)
            {
                throw new Exception("Brasserie introuvable.");
            }
            return brewery.Beers.ToList();
        }
    }
}
