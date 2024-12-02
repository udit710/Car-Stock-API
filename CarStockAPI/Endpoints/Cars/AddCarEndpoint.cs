using FastEndpoints;
using CarStockAPI.Models.Requests;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace CarStockAPI.Endpoints.Cars
{
    [Authorize]
    public class AddCarEndpoint : Endpoint<AddCarRequest>
    {
        private readonly DbContext _dbContext;

        public AddCarEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/cars");
            Description(x => x.Accepts<AddCarRequest>());
            Validator<AddCarRequestValidator>();
        }

        public override async Task HandleAsync(AddCarRequest req, CancellationToken ct)
        {
            var dealerId = int.Parse(User.Claims.First(c => c.Type == "DealerId").Value);

            using var connection = _dbContext.CreateConnection();
            var carId = await connection.ExecuteAsync(
                "INSERT INTO Cars (DealerId, Make, Model, StockCount) VALUES (@DealerId, @Make, @Model, @StockCount)",
                new { DealerId = dealerId, req.Make, req.Model, req.StockCount });

            await SendAsync(new { message = "Car added successfully.", carId }, 201, ct);
        }
    }
}
