using ErrorOr;
using MediatR;

namespace Application.Products.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
