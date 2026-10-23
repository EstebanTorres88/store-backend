using store_backend.Domain;

public class ProductMapper
{
    public static List<ProductDTO> ToDto(List<Product> products)
    {
        return products.Select(p => ToDto(p)).ToList();
    }

    public static ProductDTO ToDto(Product product)
    {
        return new ProductDTO
        {
            ProductResourceId = product.ProductResourceId,
            Name = product.Name,
        };
    }
}
