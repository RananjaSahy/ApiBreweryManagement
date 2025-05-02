using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiBreweryManagement.Application.UseCases.Beers.Queries
{
    public class BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
    }
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

        public async Task<List<BeerDto>> Handle(GetBeersByBreweryQuery query)
        {
            var brewery = await _context.Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == query.BreweryId);

            if (brewery == null)
            {
                throw new Exception("Brasserie introuvable.");
            }
            return brewery.Beers.Select(b=> new BeerDto
            {
                Id = b.Id,
                Name = b.Name,
                AlcoholContent = b.AlcoholContent,
                Price = b.Price
            }).ToList();
        }
    }
}
