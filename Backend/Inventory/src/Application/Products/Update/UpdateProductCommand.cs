using ErrorOr;
using MediatR;

namespace Application.Products.Update;

public record UpdateProductCommand(
    Guid Id,
    string ProductName,
    string ProductDescription,
    string Price,
    string Amount,
    string Unit
) : IRequest<ErrorOr<Unit>>;
