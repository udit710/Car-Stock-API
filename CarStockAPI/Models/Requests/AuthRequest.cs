using FluentValidation;

namespace CarStockAPI.Models.Requests
{
    public class AuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
