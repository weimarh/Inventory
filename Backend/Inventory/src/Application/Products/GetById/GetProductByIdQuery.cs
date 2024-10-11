using Application.Products.Common;
using ErrorOr;
using MediatR;

namespace Application.Products.GetById;

public record GetProductByIdQuery(Guid Id) : IRequest<ErrorOr<ProductResponse>>;