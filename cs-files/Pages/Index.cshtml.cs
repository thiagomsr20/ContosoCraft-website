using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.Models;
using ContosoCrafts.Services;

namespace ContosoCrafts.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public JsonFileProductService ProductService;
    public IEnumerable<Product> Produtos {get; private set;}
    public IndexModel(ILogger<IndexModel> logger, JsonFileProductService productService)
    {
        _logger = logger;
        ProductService = productService;
    }

    public void OnGet()
    {
        Produtos = ProductService.GetProducts();
    }
}
