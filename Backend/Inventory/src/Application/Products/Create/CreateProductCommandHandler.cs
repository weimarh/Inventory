using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Products.Create;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<ErrorOr<Unit>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        if (Price.Create(command.Price) is not Price price)
        {
            // return Error.Validation("Product.Price", "Invalid price");
            return ErrorsProduct.PriceWithBadFormat;
        }

        if (Quantity.Create(command.Amount, command.Unit) is not Quantity quantity)
        {
            return ErrorsProduct.QuantityWithBadFormat;
        }

        if (string.IsNullOrWhiteSpace(command.ProductName))
        {
            return ErrorsProduct.ProductNameWithBadFormat;
        }

        var product = new Product(
            new ProductId(Guid.NewGuid()),
            command.ProductName,
            command.ProductDescription,
            price,
            quantity
        );

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
