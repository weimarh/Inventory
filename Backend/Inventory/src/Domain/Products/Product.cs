using System;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Products;

public sealed class Product : AggregateRoot
{
    public Product(ProductId id, string productName, string productDescription, Price? price, Quantity quantity)
    {
        Id = id;
        ProductName = productName;
        ProductDescription = productDescription;
        Price = price;
        Quantity = quantity;
    }

    public Product()
    {
    }

    public ProductId Id { get; private set; } = null!;
    public string ProductName { get; private set; } = string.Empty;
    public string ProductDescription { get; private set; } = string.Empty;
    public Price? Price { get; private set; }
    public Quantity Quantity { get; private set; } = null!;

    public static Product UpdateProduct(
        Guid Id,
        string productName,
        string productDescription,
        Price? price,
        Quantity quantity)
    {
        return new Product(new ProductId(Id), productName, productDescription, price, quantity);
    }
}
