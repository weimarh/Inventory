using Application.Products.Common;
using ErrorOr;
using MediatR;

namespace Application.Products.GetAll;

public record GetAllProductsQuery() : IRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;
