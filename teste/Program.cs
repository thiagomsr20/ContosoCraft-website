using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using teste.model;

namespace ProgramSpace;

public class TesteModeloServices
{
    public string JsonFile => Path.Combine("Data/Teste.json");

    public List<TesteModelo> GetModelos()
    {
        using (var readFile = File.OpenRead(JsonFile))
        {
            return JsonSerializer.Deserialize<List<TesteModelo>>(readFile, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public void AddTesteModelo(string id, double value)
    {
        List<TesteModelo> TestModelList = GetModelos();
        var testeModel = new TesteModelo() { Id = id, Value = value };

        TestModelList.Add(testeModel);

        using (var outputStream = File.OpenWrite(JsonFile))
        {
            JsonSerializer.Serialize<IEnumerable<TesteModelo>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    Indented = true,
                    SkipValidation = true
                }),
                TestModelList
            );
        }
    }

    // Método RemoveTestModel ainda incompleto
    public void RemoveTestModel(string id, List<TesteModelo> testeModelosList)
    {
        var product = testeModelosList.First(product => product.Id == id);
        if (product is null) throw new ArgumentNullException();

        testeModelosList.Remove(product);

        using (var outputStream = File.OpenWrite(JsonFile))
        {
            JsonSerializer.Serialize<List<TesteModelo>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    Indented = true,
                    SkipValidation = true
                }),
                testeModelosList
            );
        }

    }
}

class ProgramSpaced
{
    static void Main(string[] args)
    {
        TesteModeloServices service = new TesteModeloServices();
        List<TesteModelo> lista = service.GetModelos();
        service.RemoveTestModel("Thiago", lista);

        foreach (var itens in lista)
        {
            Console.WriteLine(itens.Id);
            Console.WriteLine(itens.Value);
        }
    }
}