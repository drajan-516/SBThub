using FluentValidation;
using SBThub.Domain.ValueObjects.Product;

namespace SBThub.Application.UseCases.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Request.FullTitle)
            .NotEmpty().WithMessage("Title is important!!!!")
            .MaximumLength(ProductTitle.MaxLength);
    }
}