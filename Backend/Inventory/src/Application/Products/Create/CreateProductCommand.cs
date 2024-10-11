using ErrorOr;
using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(
    string ProductName,
    string ProductDescription,
    string Price,
    string Amount,
    string Unit) : IRequest<ErrorOr<Unit>>;
