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

        // Apontando o Get do arquivo JSON para a pasta wwwroot/data/product.json
        // wwwroot/data/products.json
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); 

        // Retorna uma lista enumerável de produtos no formato JSON
        public List<Product>? GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, WriteIndented = true};

                return JsonSerializer.Deserialize<List<Product>>(jsonFileReader.ReadToEnd(), options);
            }
        }

        public void AddRating(string productId, int rating)
        {
            IEnumerable<Product>? products = GetProducts();
            if(products is null) throw new Exception("Null database!!");

            var product = products.First(x => x.Id == productId);

            if(product.Ratings is null)
            {
                product.Ratings = new(rating);
            }

            product.Ratings.Add(rating);

            // Registrar a avaliação no database (JSON file)
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        Indented = true,
                        SkipValidation = true
                    }),
                    products
                );
            }

        }
    }
}