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
}