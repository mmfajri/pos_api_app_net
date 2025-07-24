using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.DTOs.ProductDTO;
using FluentValidation;

namespace pos_api_app.Utilities.Validations.ProductDTO;

public class NewProductValidation : AbstractValidator<NewProductDTO>
{
    private readonly IProductRepository _productRepository;

    public NewProductValidation(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(attr => attr.BarcodeID)
            .NotEmpty().WithMessage("BarcodeID is required")
            .Must(UniqueBarcode).WithMessage("Product was already registered");

        RuleFor(attr => attr.Title)
            .NotEmpty().WithMessage("Title is required");
    }

    public bool UniqueBarcode(string property)
    {
        return !_productRepository.UniqueBarcode(property);
    }
}
