using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using teste.model;

namespace ProgramSpace;

public class testeModeloServices
{
    public string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string jsonFile
    {
        get { return Path.Combine(desktopPath, "github", "contosocraft-website", "teste", "teste.json"); }
    }

    public IEnumerable<testeModelo>? GettesteModelo()
    {
        using (var readFile = File.OpenRead(jsonFile))
        {
            return JsonSerializer.Deserialize<testeModelo[]>(readFile, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    // public void AddtesteModelo(string id, double value)
    // {
    //     IEnumerable<testeModelo>? testeModelo = GettesteModelo();

    //     using (var outputStream = File.OpenWrite(jsonFile))
    //     {
    //         JsonSerializer.Serialize(testeModelo, outputStream);
    //     }

    // }
}

class ProgramSpace
{
    static void Main(string[] args)
    {
        testeModeloServices service = new testeModeloServices();

        foreach (var testeModelo in service.GettesteModelo())
        {
            Console.WriteLine(testeModelo.id);
            Console.WriteLine(testeModelo.value);
        }
        Console.WriteLine($"That is your path: {service.jsonFile}");
        
    }
}