using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using teste.model;
using System.IO;

namespace ProgramSpace;

public class TesteModeloServices
{
    // public string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string JsonFile => Path.Combine("Data/Teste.json");

    // Reescrevendo método ToString, para facilitar a visualização do database
    public override string ToString() => JsonSerializer.Serialize<List<TesteModelo>>(GetModelos());

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
    public void RemoveTestModel(string id)
    {
        if(string.IsNullOrEmpty(id)) throw new Exception("Null or empty string input");

        List<TesteModelo> TestModelList = GetModelos();
        TestModelList.Remove(TestModelList.First(product => product.Id == id));
    }
}


class ProgramSpace
{
    static void Main(string[] args)
    {
        TesteModeloServices service = new TesteModeloServices();
        List<TesteModelo> lista = service.GetModelos();
        // service.RemoveTestModel("Marlon");
        // service.RemoveTestModel("Thiago");
        // service.RemoveTestModel("");
        
        // Tentando verificar o que está sendo retornado e como está funcionando o teste com JSON
        Console.WriteLine(lista.ToString());

        // foreach (var testeModelo in service.GetModelos())
        // {
        //     Console.WriteLine(testeModelo.Id);
        //     Console.WriteLine(testeModelo.Value);
        // }
        // Console.WriteLine($"That is your path: {service.JsonFile}");

    }
}