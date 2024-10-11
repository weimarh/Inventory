using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Products.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(new ProductId(request.Id)))
        {
            return ErrorsProduct.ProductNotFound;
        }

        if (Price.Create(request.Price) is not Price)
        {
            return ErrorsProduct.PriceWithBadFormat;
        }

        if (Quantity.Create(request.Amount, request.Unit) is not Quantity)
        {
            return ErrorsProduct.QuantityWithBadFormat;
        }

        Product product = Product.UpdateProduct(
            request.Id,
            request.ProductName,
            request.ProductDescription,
            price: Price.Create(request.Price),
            quantity: Quantity.Create(request.Amount, request.Unit)
        );

        _repository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
