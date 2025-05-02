using ApiBreweryManagement.Application.UseCases.Wholesalers.Queries.GetQuote;
using ApiBreweryManagement.Domain.Entities;
using ApiBreweryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class GetQuoteHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetQuoteHandler _handler;

    public GetQuoteHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _handler = new GetQuoteHandler(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var beer = new Beer { Id = 1, Name="Test Name", AlcoholContent = 32, Price = 3.50m };
        var wholesaler = new Wholesaler
        {
            Id = 1,
            Name = "Test Wholesaler",
            WholesalerStocks = new List<WholesalerStock>
            {
                new WholesalerStock { BeerId = 1, Quantity = 10, Beer = beer }
            }
        };

        _context.Beers.Add(beer);
        _context.Wholesalers.Add(wholesaler);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_ShouldReturnTotalPrice_WhenSingleBeerOrder()
    {
        var query = new GetQuoteQuery
        {
            WholesalerId = 1,
            Orders = new List<OrderItem>
            {
                new OrderItem { BeerId = 1, Quantity = 5 }
            }
        };

        var result = await _handler.Handle(query);

        Assert.Equal(17.50m, result);
    }
}