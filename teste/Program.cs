using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using teste.model;

namespace ProgramSpace;

public class TesteModeloServices
{
    public string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string JsonFile => Path.Combine(DesktopPath, "github", "contosocraft-website", "teste", "teste.json");

    public List<TesteModelo> GetModelos()
    {
        using (var readFile = File.OpenRead(JsonFile))
        {
            return JsonSerializer.Deserialize<List<TesteModelo>>(readFile, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public void AddTesteModelo(string id, double value)
    {
        List<TesteModelo> testeModeloInstancia = GetModelos();
        var testeModel = new TesteModelo() { Id = id, Value = value };

        testeModeloInstancia.Add(testeModel);

        using (var outputStream = File.OpenWrite(JsonFile))
        {
            JsonSerializer.Serialize<IEnumerable<TesteModelo>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    Indented = true,
                    SkipValidation = true
                }),
                testeModeloInstancia
            );
        }
    }
}


class ProgramSpace
{
    static void Main(string[] args)
    {
        TesteModeloServices service = new TesteModeloServices();
        service.AddTesteModelo("Marlon", 4000.00);
        service.AddTesteModelo("Thiago", 2000.00);

        foreach (var testeModelo in service.GetModelos())
        {
            Console.WriteLine(testeModelo.Id);
            Console.WriteLine(testeModelo.Value);
        }
        Console.WriteLine($"That is your path: {service.JsonFile}");

    }
}