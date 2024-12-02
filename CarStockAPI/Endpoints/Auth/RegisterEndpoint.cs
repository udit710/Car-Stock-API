using FastEndpoints;
using CarStockAPI.Models.Requests;
using CarStockAPI.Data;
using Dapper;

namespace CarStockAPI.Endpoints.Auth
{
    public class RegisterEndpoint : Endpoint<AuthRequest>
    {
        private readonly DbContext _dbContext;

        public RegisterEndpoint(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/auth/register");
            Description(x => x.Accepts<AuthRequest>());
            AllowAnonymous();
            Validator<AuthRequestValidator>();
        }

        public override async Task HandleAsync(AuthRequest req, CancellationToken ct)
        {
            using var connection = _dbContext.CreateConnection();
            var existingDealer = await connection.QuerySingleOrDefaultAsync("SELECT * FROM Dealers WHERE Username = @Username", new { req.Username });

            if (existingDealer != null)
            {
                AddError("Username already exists.");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await connection.ExecuteAsync("INSERT INTO Dealers (Username, Password) VALUES (@Username, @Password)", new { req.Username, req.Password });

            await SendAsync(new { message = "Dealer registered successfully." }, 201, ct);
        }
    }
}
