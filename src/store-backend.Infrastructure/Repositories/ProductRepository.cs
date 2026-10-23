using Microsoft.EntityFrameworkCore;
using store_backend.Domain;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.ProductResourceId == productId);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        return product;
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
    }
}
