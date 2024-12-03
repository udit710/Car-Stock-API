using FastEndpoints;
using CarStockAPI.Models.Requests;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace CarStockAPI.Endpoints.Cars
{
    [Authorize]
    public class UpdateStockEndpoint : Endpoint<UpdateStockRequest>
    {
        private readonly DbContext _dbContext;

        public UpdateStockEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.PUT);
            Routes("/cars/{CarId}/stock");
            Validator<UpdateStockRequestValidator>();
            Description(x => x.Accepts<UpdateStockRequest>());
        }

        public override async Task HandleAsync(UpdateStockRequest req, CancellationToken ct)
        {
            var carId = Route<int>("CarId");
            var dealerId = int.Parse(User.Claims.First(c => c.Type == "DealerId").Value);
            // Console.WriteLine("CarId: " + carId);
            // Console.WriteLine("DealerId: " + dealerId);
            // Console.WriteLine("StockCount: " + req.StockCount);

            using var connection = _dbContext.CreateConnection();
            var result = await connection.ExecuteAsync(
                "UPDATE Cars SET StockCount = @StockCount WHERE CarId = @CarId AND DealerId = @DealerId",
                new { req.StockCount, CarId = carId, DealerId = dealerId });

            if (result == 0)
            {
                AddError("Car not found.");
                await SendErrorsAsync(statusCode: 404, cancellation: ct);
                return;
            }

            await SendAsync(new { message = "Stock level updated successfully." }, cancellation: ct);
        }
    }
}
