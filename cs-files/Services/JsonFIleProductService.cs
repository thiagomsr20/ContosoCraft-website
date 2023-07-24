using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            // Apontando o Get do arquivo JSON para a pasta wwwroot/data/product.json
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); } // wwwroot/data/products.json
        }

        // Retorna uma lista enumerável de produtos no formato JSON
        public IEnumerable<Product>? GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, WriteIndented = true};

            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(), options);
        }
    }
}