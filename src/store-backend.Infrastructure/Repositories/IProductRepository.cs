using store_backend.Domain;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product> AddAsync(Product product);
    Task DeleteAsync(Product product);
}
