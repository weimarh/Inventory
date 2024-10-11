using Application.Products.Delete;
using Domain.Primitives;
using Domain.Products;

namespace Application.Products.UnitTests.Delete;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests(
        Mock<IUnitOfWork> unitOfWorkMock,
        Mock<IProductRepository> productRepositoryMock,
        DeleteProductCommandHandler handler)
    {
        _unitOfWorkMock = unitOfWorkMock;
        _productRepositoryMock = productRepositoryMock;
        _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleDeleteProductCommand_WhenIdIsNotFound_ShouldReturnValidationError()
    {
        // Given
        DeleteProductCommand command = new(Guid.NewGuid());

        // When
        var result = await _handler.Handle(command, default);

        // Then
        result.IsError.Should().BeTrue();
    }
}
