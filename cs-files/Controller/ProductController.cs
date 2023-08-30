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

    public JsonFileProductService ProductService { get; set;}

    [HttpGet]
    public List<Product>? Get()
    {
        return ProductService.GetProducts();
    }

    [Route("rate")]
    [HttpPut]
    // localhost:5262/product/rate?productid="jenlooper-cactus"&rating=4
    public ActionResult Put(
        [FromQuery] string productId,
        [FromQuery] int rating)
    {
        var product = ProductService.GetProducts().FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            return NotFound();
        }

        ProductService.AddRating(productId, rating);

        return Ok();
    }
}