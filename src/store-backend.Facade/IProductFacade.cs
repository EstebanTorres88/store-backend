public interface IProductFacade
{
    Task<List<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByIdAsync(Guid productId);
    Task<ProductDTO> AddAsync(ProductDTO product);
    Task DeleteAsync(Guid productId);
}
