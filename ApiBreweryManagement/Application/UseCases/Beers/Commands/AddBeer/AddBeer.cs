using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Domain.Entities;

namespace ApiBreweryManagement.Application.UseCases.Beers.Commands.AddBeer
{
    public class AddBeerCommand
    {
        public string Name { get; set; } = null!;
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }

    }

    public class AddBeerHandler
    {
        private readonly IApplicationDbContext _context;

        public AddBeerHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddBeerCommand command)
        {
            var brewery = await _context.Breweries.FindAsync(command.BreweryId);
            if (brewery == null)
            {
                throw new Exception("Brasserie introuvable.");
            }
            var beer = new Beer
            {
                Name = command.Name,
                AlcoholContent = command.AlcoholContent,
                Price = command.Price,
                BreweryId = command.BreweryId
            };

            brewery.AddBeer(beer);
            await _context.SaveChangesAsync();
        }
    }

}
