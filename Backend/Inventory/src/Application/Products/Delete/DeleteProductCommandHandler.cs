using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using ErrorOr;
using MediatR;

namespace Application.Products.Delete;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(new ProductId(request.Id)) is not Product product)
        {
            //return Error.NotFound("Product.NotFound", "Product not found");
            return ErrorsProduct.ProductNotFound;
        }

        _repository.Delete(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
