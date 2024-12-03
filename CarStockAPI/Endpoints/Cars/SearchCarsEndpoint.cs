using FastEndpoints;
using CarStockAPI.Models;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using CarStockAPI.Models;

namespace CarStockAPI.Endpoints.Cars
{
    [Authorize]
    public class SearchCarsEndpoint : EndpointWithoutRequest<List<Car>>
    {
        private readonly DbContext _dbContext;

        public SearchCarsEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/cars/search");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var dealerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DealerId");
            if (dealerIdClaim == null)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            if (!int.TryParse(dealerIdClaim.Value, out int dealerId))
            {
                await SendUnauthorizedAsync(ct);
                return;
            }
            var Make = Query<string>("Make");
            var Model = Query<string>("Model");

            using var connection = _dbContext.CreateConnection();
            var query = "SELECT * FROM Cars WHERE DealerId = @DealerId";
            var parameters = new DynamicParameters();
            parameters.Add("DealerId", dealerId);

            if (!string.IsNullOrEmpty(Make))
            {
                query += " AND Make = @Make";
                parameters.Add("Make", Make);
            }
            if (!string.IsNullOrEmpty(Model))
            {
                query += " AND Model = @Model";
                parameters.Add("Model", Model);
            }

            var cars = await connection.QueryAsync<Car>(query, parameters);
            await SendAsync(cars.ToList(), cancellation: ct);
        }
    }
}
