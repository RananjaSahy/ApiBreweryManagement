using ApiBreweryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBreweryManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Brewery> Breweries { get; }
        DbSet<Beer> Beers { get; }
        DbSet<Wholesaler> Wholesalers { get; }
        DbSet<WholesalerStock> WholesalerStocks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
