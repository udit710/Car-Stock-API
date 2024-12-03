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
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int StockCount { get; set; }
    }

    public class AddCarRequestValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarRequestValidator()
        {
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Year).InclusiveBetween(1900, 2024);
            RuleFor(x => x.StockCount).GreaterThanOrEqualTo(0);
        }
    }
}
