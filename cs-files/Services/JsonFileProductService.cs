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
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, WriteIndented = true};

                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(), options);
            }
        }

        public void AddRating(string productId, int rating)
        {
            IEnumerable<Product> products = GetProducts();

            Product product = products.First(x => x.Id == productId);

            if(product.Ratings == null) product.Ratings = new int[] { rating };

            else
            {
                var ratings = product.Ratings.ToList();
                // Inserir nova avaliação, caso já haver uma avaliação anterior
                ratings.RemoveAll();
                ratings.Add(rating);

                ratings.ToArray();
            }

            // Registrar a avaliação no database (JSON file)
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream,new JsonWriterOptions{
                        Indented = true,
                        SkipValidation = true
                    }),
                    products
                )
            }

        }
    }
}