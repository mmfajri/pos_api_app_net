using pos_api_app.DTOs.PriceDTO;
using FluentValidation;

namespace pos_api_app.Utilities.Validations.PriceDTO;

public class NewPriceValidation : AbstractValidator<NewPriceDTO>
{
    public NewPriceValidation()
    {
        RuleFor(attr => attr.UnitName).NotEmpty().WithMessage("UnitName is required");
        RuleFor(attr => attr.Amount)
            .NotEmpty()
            .WithMessage("Amount is required")
            .GreaterThanOrEqualTo(0.0m)
            .WithMessage("Amount must be greater or equal to 0.0");
    }
}
