using FastEndpoints;
using CarStockAPI.Models.Requests;
using Dapper;
using CarStockAPI.Helpers;
using CarStockAPI.Data;
using CarStockAPI.Models;

namespace CarStockAPI.Endpoints.Auth
{
    public class LoginEndpoint : Endpoint<AuthRequest, AuthResponse>
    {
        private readonly DbContext _dbContext;
        private readonly string _jwtKey;

        public LoginEndpoint(DbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _jwtKey = configuration["Jwt:Key"];
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/auth/login");
            Description(x => x.Accepts<AuthRequest>());
            AllowAnonymous();
            Validator<AuthRequestValidator>();
        }

        
        public override async Task HandleAsync(AuthRequest req, CancellationToken ct)
        {

            // Authenticate user
            using var connection = _dbContext.CreateConnection();
            var dealer = await connection.QuerySingleOrDefaultAsync("SELECT * FROM Dealers WHERE Username = @Username", new { req.Username });

            if (dealer == null || req.Password != dealer.Password)
            {
                AddError("Invalid username or password.");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            int did = (int)dealer.DealerId;
            // Generate JWT token
            var token = JwtHelper.GenerateToken(did, _jwtKey);

            // Send response
            await SendAsync(new AuthResponse { Token = token }, cancellation: ct);
        }
    }
}
