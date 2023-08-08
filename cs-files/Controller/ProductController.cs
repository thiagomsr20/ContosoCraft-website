using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ContosoCrafts.Services;
using ContosoCrafts.Models;

namespace ContosoCrafts.Controller;

[Route("product")]
[ApiController]
public class ProductController : ControllerBase
{
    public ProductController(JsonFileProductService productService)
    {
        this.ProductService = productService;
    }

    public JsonFileProductService ProductService { get; }

    [HttpGet]
    public IEnumerable<Product>? Get()
    {
        return ProductService.GetProducts();
    }

    [Route("rate")]
    [HttpGet]
    public ActionResult Get(
        [FromQuery] string productId, 
        [FromQuery] int rating
    )
    {
        var product = ProductService.GetProducts().FirstOrDefault(x => x.Id == productId);
        if (product is null) return NotFound();

        ProductService.AddRating(productId, rating);

        return Ok();
    }
}