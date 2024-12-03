using FluentValidation;

namespace CarStockAPI.Models.Requests
{
    /**
     * AddCarRequest model: Represents a request to add a car to the database
     * Attributes:
     *  - Make: The make of the car
     *  - Model: The model of the car
     *  - Year: The year the car was made
     *  - StockCount: The number of cars in stock
     */
    public class AddCarRequest
    {
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required int Year { get; set; }
        public int StockCount { get; set; }
    }

    public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarRequestValidator()
        {
            RuleFor(x => x.Make)
                .NotEmpty()
                .WithMessage("Make is a required field.");
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is a required field.");
            RuleFor(x => x.Year)
                .InclusiveBetween(1900, 2024)
                .WithMessage("Year must be between 1900 and 2024.");
            RuleFor(x => x.StockCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock count must be greater than or equal to 0.");
        }
    }
}
