public class ProductFacade : IProductFacade
{
    public readonly IProductService _productService;
    public readonly AppDbContext _context;

    public ProductFacade(IProductService productService, AppDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    public async Task<List<ProductDTO>> GetAllAsync()
    {
        var entities = await _productService.GetAllAsync();
        return ProductMapper.ToDto(entities);
    }

    public async Task<ProductDTO> GetByIdAsync(Guid productId)
    {
        var entity = await _productService.GetByIdAsync(productId);
        if (entity == null)
            throw new ResourceNotFoundException();
        return ProductMapper.ToDto(entity);
    }

    public async Task<ProductDTO> AddAsync(ProductDTO product)
    {
        var entity = await _productService.AddAsync(product);
        await _context.SaveChangesAsync();
        return ProductMapper.ToDto(entity);
    }

    public async Task DeleteAsync(Guid productId)
    {
        await _productService.DeleteAsync(productId);
        await _context.SaveChangesAsync();
    }
}
