using FluentValidation;

namespace Application.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r => r.ProductName)
            .NotEmpty()
            .MaximumLength(60)
            .WithName("Name");

        RuleFor(r => r.ProductDescription)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Description");

        RuleFor(r => r.Amount)
            .NotEmpty()
            .WithName("Amount");

        RuleFor(r => r.Unit)
            .NotEmpty()
            .WithName("Unit");
    }
}
