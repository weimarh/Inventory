using Application.Products.Common;
using Domain.Products;
using ErrorOr;
using MediatR;

namespace Application.Products.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<IReadOnlyList<ProductResponse>>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await _repository.GetAll();

        return products.Select(product => new ProductResponse(
            product.Id.Value,
            product.ProductName,
            product.ProductDescription,
            product.Price.Value,
            new QuantityResponse(
                product.Quantity.Amount,
                product.Quantity.Unit
            )
        )).ToList();
    }
}
