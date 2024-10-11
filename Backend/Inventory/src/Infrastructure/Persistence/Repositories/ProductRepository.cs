using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Product product) => _context.Products.AddAsync(product);

    public void Delete(Product product) => _context.Products.Remove(product);

    public void Update(Product product) => _context.Products.Update(product);

    public async Task<bool> ExistsAsync(ProductId id) => await _context.Products.AnyAsync(product => product.Id == id);

    public async Task<Product?> GetByIdAsync(ProductId id) => await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

    public async Task<List<Product>> GetAll() => await _context.Products.ToListAsync();
}
