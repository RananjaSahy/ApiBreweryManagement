using ApiBreweryManagement.Application.Common.Interfaces;

namespace ApiBreweryManagement.Application.UseCases.Beers.Commands.DeleteBeer
{
    public class DeleteBeerCommand
    {
        public int BeerId { get; set; }
        public int BreweryId { get; set; }
    }

    public class DeleteBeerHandler
    {
        private readonly IApplicationDbContext _context;

        public DeleteBeerHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteBeerCommand command)
        {
            var brewery = await _context.Breweries.FindAsync(command.BreweryId);
            if (brewery == null)
            {
                throw new Exception("Brasserie introuvable.");
            }
            var beer = await _context.Beers.FindAsync(command.BeerId);
            if (beer == null)
            {
                throw new Exception("Bière introuvable.");
            }
            brewery.RemoveBeer(beer);
            await _context.SaveChangesAsync();
        }
    }
}
