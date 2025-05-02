using ApiBreweryManagement.Application.Common.Interfaces;
using ApiBreweryManagement.Application.UseCases.Beers.Commands.AddBeer;
using ApiBreweryManagement.Application.UseCases.Beers.Commands.DeleteBeer;
using ApiBreweryManagement.Application.UseCases.Beers.Queries;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.AddBeerToWholesaler;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Commands.UpdateBeerStock;
using ApiBreweryManagement.Application.UseCases.Wholesalers.Queries.GetQuote;
using ApiBreweryManagement.Infrastructure.Interceptors;
using ApiBreweryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<EntityBaseAuditableInterceptor>();
builder.Services.AddScoped<AddBeerHandler>();
builder.Services.AddScoped<DeleteBeerHandler>();
builder.Services.AddScoped<GetBeersByBreweryHandler>();
builder.Services.AddScoped<AddBeerToWholesalerHandler>();
builder.Services.AddScoped<UpdateBeerStockHandler>();
builder.Services.AddScoped<GetQuoteHandler>();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<EntityBaseAuditableInterceptor>();
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(interceptor);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
