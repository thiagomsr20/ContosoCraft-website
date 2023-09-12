using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ContosoCrafts.Services;
using ContosoCrafts.Models;
using System.Net;

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
    public ActionResult<List<Product>> Get()
    {
        if(ProductService.GetProducts() is null) return NotFound();

        return Ok(ProductService.GetProducts());
    }

    [Route("rate")]
    [HttpPut("{id}")]
    // localhost:5262/product/rate?productid=jenlooper-cactus&rating=4
    public ActionResult Put(
        [FromQuery] string productId,
        [FromQuery] int rating)
    {
        List<Product>? products = ProductService.GetProducts();

        // S[o retorna a mensagem, verificar maneira de retornar mensagem e ActionResult default return
        if(products is null) return NotFound("null return on get database");

        Product? product = products.FirstOrDefault(x => x.Id == productId);
        if(product == null)
        {
            return NotFound("Null return on get a specific product by ID");
        }

        ProductService.AddRating(productId, rating);

        return Ok();
    }
}