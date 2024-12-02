using FastEndpoints;
using CarStockAPI.Models;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using CarStockAPI.Models;

namespace CarStockAPI.Endpoints.Cars
{
    [Authorize]
    public class ListCarsEndpoint : EndpointWithoutRequest<List<Car>>
    {
        private readonly DbContext _dbContext;

        public ListCarsEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/cars");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var dealerId = int.Parse(User.Claims.First(c => c.Type == "DealerId").Value);

            using var connection = _dbContext.CreateConnection();
            var cars = await connection.QueryAsync<Car>("SELECT * FROM Cars WHERE DealerId = @DealerId", new { DealerId = dealerId });

            await SendAsync(cars.ToList(), cancellation: ct);
        }
    }
}
