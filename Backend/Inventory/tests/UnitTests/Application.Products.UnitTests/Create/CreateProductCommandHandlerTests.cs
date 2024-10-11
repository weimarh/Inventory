using Application.Products.Create;
using Domain.Primitives;
using Domain.Products;
using Domain.DomainErrors;


namespace Application.Products.UnitTests.Create;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockPoductRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateProductCommandHandler _handler;
    public CreateProductCommandHandlerTests()
    {
        _mockPoductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateProductCommandHandler(_mockPoductRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateProductCommand_WhenPriceHasBadFormat_ShouldReturnValidationError()
    {
        //Arrange
        CreateProductCommand command = new CreateProductCommand(
            "Avena",
            "avena para desayuno",
            "25.30a",
            "1",
            "cuartilla");

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(ErrorsProduct.PriceWithBadFormat.Code);
        result.FirstError.Description.Should().Be(ErrorsProduct.PriceWithBadFormat.Description);
    }

    [Fact]
    public async Task HandleCreateProductCommand_WhenQuantityHasBadFormat_ShouldReturnValidationError()
    {
        // Arrange
        CreateProductCommand command = new CreateProductCommand(
            "Avena",
            "avena para desayuno",
            "25.30",
            "1a",
            "cuartilla");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(ErrorsProduct.QuantityWithBadFormat.Code);
        result.FirstError.Description.Should().Be(ErrorsProduct.QuantityWithBadFormat.Description);
    }

    [Fact]
    public async Task HandleCreateProductCommand_WhenNameIsEmpty_ShouldReturnValidationError()
    {
        // Arrange
        CreateProductCommand command = new CreateProductCommand(
            "",
            "avena para desayuno",
            "25.30",
            "1",
            "cuartilla");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(ErrorsProduct.ProductNameWithBadFormat.Code);
        result.FirstError.Description.Should().Be(ErrorsProduct.ProductNameWithBadFormat.Description);
    }

    [Fact]
    public async Task HandleCreateProductCommand_WhenUnitIsEmpty_ShouldReturnValidationError()
    {
        // Arrange
        CreateProductCommand command = new CreateProductCommand(
            "avena",
            "avena para desayuno",
            "25.30",
            "1",
            "");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(ErrorsProduct.QuantityUnitWithBadFormat.Code);
        result.FirstError.Description.Should().Be(ErrorsProduct.QuantityUnitWithBadFormat.Description);
    }
}