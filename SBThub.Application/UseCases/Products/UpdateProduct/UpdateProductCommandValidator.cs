using FluentValidation;
using SBThub.Domain.ValueObjects.Product;

namespace SBThub.Application.UseCases.Products.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Uuid)
            .NotEmpty().WithMessage("Идентификатор пользователя обязательно.");
        RuleFor(command => command.Request.FullTitle)
            .NotEmpty().WithMessage("Title is important!!!!")
            .MaximumLength(ProductTitle.MaxLength);
    }
}