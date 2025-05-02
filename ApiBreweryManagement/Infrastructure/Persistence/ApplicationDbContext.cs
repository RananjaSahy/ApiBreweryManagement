using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBreweryManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Beer>()
                .HasOne(b => b.Brewery)
                .WithMany(br => br.Beers)
                .HasForeignKey(b => b.BreweryId);

            modelBuilder.Entity<WholesalerStock>()
                .HasOne(ws => ws.Wholesaler)
                .WithMany(w => w.WholesalerStocks)
                .HasForeignKey(ws => ws.WholesalerId);

            modelBuilder.Entity<WholesalerStock>()
                .HasOne(ws => ws.Beer)
                .WithMany(b => b.WholesalerStocks)
                .HasForeignKey(ws => ws.BeerId);
        }

    }
}
