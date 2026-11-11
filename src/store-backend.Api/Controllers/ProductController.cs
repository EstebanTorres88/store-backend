using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productFacade.GetAllAsync();
        var models = ProductMapper.ToModel(products);
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProduct(Guid id)
    {
        try
        {
            var product = await _productFacade.GetByIdAsync(id);
            var model = ProductMapper.ToModel(product);
            return Ok(model);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] CreateProductRequestModel product)
    {
        var dto = ProductMapper.toDto(product);
        var addedProduct = await _productFacade.AddAsync(dto);
        var model = ProductMapper.ToModel(addedProduct);
        return CreatedAtAction(nameof(GetProduct), new { id = model.ProductResourceId }, model);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        try
        {
            await _productFacade.DeleteAsync(id);
            return NoContent();
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }
}
