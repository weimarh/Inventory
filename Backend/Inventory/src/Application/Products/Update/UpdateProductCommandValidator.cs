using FluentValidation;

namespace Application.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
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
