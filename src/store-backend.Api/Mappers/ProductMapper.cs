public class ProductMapper
{
    public static ProductDTO toDto(CreateProductRequestModel model)
    {
        return new ProductDTO
        {
            ProductResourceId = model.ProductResourceId!.Value,
            Name = model.Name,
        };
    }

    public static List<ProductResponseModel> ToModel(List<ProductDTO> products)
    {
        return products.Select(p => ToModel(p)).ToList();
    }

    public static ProductResponseModel ToModel(ProductDTO product)
    {
        return new ProductResponseModel
        {
            ProductResourceId = product.ProductResourceId,
            Name = product.Name,
        };
    }
}
