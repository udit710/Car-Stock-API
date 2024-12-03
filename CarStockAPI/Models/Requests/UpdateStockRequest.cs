using FluentValidation;

namespace CarStockAPI.Models.Requests
{
    public class UpdateStockRequest
    {
        public int StockCount { get; set; }
    }

    public class UpdateStockRequestValidator : AbstractValidator<UpdateStockRequest>
    {
        public UpdateStockRequestValidator()
        {
            RuleFor(x => x.StockCount).GreaterThanOrEqualTo(0);
        }
    }
}
