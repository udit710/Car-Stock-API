using FluentValidation;

namespace CarStockAPI.Models.Requests
{
    public class AddCarRequest
    {
        public int Make { get; set; }
        public string Model { get; set; }
        public int StockCount { get; set; }
    }

    public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarRequestValidator()
        {
            RuleFor(x => x.Make).GreaterThan(0);;
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.StockCount).GreaterThanOrEqualTo(0);
        }
    }
}
