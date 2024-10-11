using ErrorOr;

namespace Domain.DomainErrors;

public static class ErrorsProduct
{
    public static Error PriceWithBadFormat => Error.Validation(
        code: "Product.Price",
        description: "Price with bad format");

    public static Error QuantityWithBadFormat => Error.Validation(
        code: "Product.Quantity",
        description: "Quantity with bad format");

    public static Error ProductNotFound => Error.NotFound(
        code: "Product.NotFound",
        description: "Product with the given id was not found");

    public static Error ProductNameWithBadFormat => Error.Validation(
        code: "Product.Name",
        description: "The procuct name can't be empty");

    public static Error QuantityUnitWithBadFormat => Error.Validation(
        code: "Quantity.Unit",
        description: "The quantity unit can't be empty");
}
