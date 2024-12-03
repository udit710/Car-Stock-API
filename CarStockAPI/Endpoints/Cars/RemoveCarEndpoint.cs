using FastEndpoints;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace CarStockAPI.Endpoints.Cars
{
    [Authorize]
    public class RemoveCarEndpoint : EndpointWithoutRequest
    {
        private readonly DbContext _dbContext;

        public RemoveCarEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.DELETE);
            Routes("/cars/{CarId}");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            try
            {
                var carId = Route<int>("CarId");
                var dealerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DealerId");
                if (dealerIdClaim == null || !int.TryParse(dealerIdClaim.Value, out int dealerId))
                {
                    await SendUnauthorizedAsync(ct);
                    return;
                }

                using var connection = _dbContext.CreateConnection();
                // Console.WriteLine("CarId: " + carId);
                // Console.WriteLine("DealerId: " + dealerId);
                var result = await connection.ExecuteAsync(
                    "DELETE FROM Cars WHERE CarId = @CarId AND DealerId = @DealerId",
                    new { CarId = carId, DealerId = dealerId });

                if (result == 0)
                {
                    AddError("Car not found.");
                    await SendErrorsAsync(statusCode: 404, cancellation: ct);
                    return;
                }

                await SendAsync(new { message = "Car removed successfully." }, cancellation: ct);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                await SendErrorsAsync(statusCode: 500, cancellation: ct);
            }
        }

    }
}
