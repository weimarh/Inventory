using Application.Products.Common;
using Domain.DomainErrors;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _productRepository.GetByIdAsync(new ProductId(query.Id)) is not Product product)
        {
            return ErrorsProduct.ProductNotFound;
        }

        return new ProductResponse(
            product.Id.Value,
            product.ProductName,
            product.ProductDescription,
            product.Price.Value,
            new QuantityResponse(
                product.Quantity.Amount,
                product.Quantity.Unit
            )
        );
    }
}
