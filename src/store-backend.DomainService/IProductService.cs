using store_backend.Domain;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product> AddAsync(ProductDTO product);
    Task DeleteAsync(Guid productId);
}
