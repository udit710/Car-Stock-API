using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarStockAPI.Data;
using CarStockAPI.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new InvalidOperationException("JWT Key is not configured.");
    }
    var key = Encoding.ASCII.GetBytes(jwtKey);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Initialize Database
var dbInitializer = app.Services.GetRequiredService<DbContext>();
dbInitializer.Initialize();
var dbPopulator = new PopulateDb(dbInitializer);
// dbPopulator.populate();
// Configure Middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

// Run the application
app.Run();
