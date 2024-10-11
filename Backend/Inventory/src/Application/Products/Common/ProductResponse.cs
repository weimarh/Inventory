namespace Application.Products.Common;

public record ProductResponse(
    Guid Id,
    string ProductName,
    string ProductDescription,
    string Price,
    QuantityResponse Quantity
);

public record QuantityResponse(
    string Amount,
    string Unit
);