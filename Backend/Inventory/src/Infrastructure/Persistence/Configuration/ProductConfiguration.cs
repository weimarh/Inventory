using Domain.Products;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            productId => productId.Value,
            value => new ProductId(value)
        );

        builder.Property(x => x.ProductName).HasMaxLength(60);

        builder.Property(x => x.ProductDescription).HasMaxLength(200);

        builder.Property(x => x.Price).HasConversion(
            price => price!.Value,
            value => Price.Create(value)
        );

        builder.OwnsOne(x => x.Quantity, quantityBuilder => {
            quantityBuilder.Property(q => q.Amount).IsRequired(true);
            quantityBuilder.Property(q => q.Unit).IsRequired(true).HasMaxLength(10);
        });
    }
}
