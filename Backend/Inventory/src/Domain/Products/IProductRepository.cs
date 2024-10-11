namespace Domain.Products;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product?> GetByIdAsync(ProductId id);
    Task<bool> ExistsAsync(ProductId id);
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}
