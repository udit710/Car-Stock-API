using FastEndpoints;
using CarStockAPI.Models.Requests;
using CarStockAPI.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.Results;

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
            var dealerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DealerId"); // Get the dealerId claim
            if (dealerIdClaim == null) // If the dealerId claim is not found, return unauthorized
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            if (!int.TryParse(dealerIdClaim.Value, out int dealerId)) // If the dealerId claim is not an integer, return unauthorized
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            using var connection = _dbContext.CreateConnection();

            var duplicateCar = await connection.QueryFirstOrDefaultAsync<int>(
                "SELECT COUNT(*) FROM Cars WHERE DealerId = @DealerId AND Make = @Make AND Model = @Model AND Year = @Year",
                new { DealerId = dealerId, req.Make, req.Model, req.Year });

            if (duplicateCar > 0) // If the car already exists, return a conflict
            {
                AddError("Car already exists. If you wish to update the stock count, use the /cars/{CarId}/stock endpoint.");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            var carId = await connection.ExecuteAsync(
                "INSERT INTO Cars (DealerId, Make, Model, Year, StockCount) VALUES (@DealerId, @Make, @Model, @Year, @StockCount)",
                new { DealerId = dealerId, req.Make, req.Model, req.Year, req.StockCount });

            // Get the carId of the newly added car
            carId = await connection.QueryFirstOrDefaultAsync<int>("SELECT last_insert_rowid()"); // Get the latest inserted CarId

            await SendAsync(new { message = "Car added successfully.", carId }, 201, ct);
        }
    }
}
