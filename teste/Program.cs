using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgramSpace;

public class testeModels
{
    [JsonPropertyName("id")]
    public string id { get; set; }

    [JsonPropertyName("value")]
    public double value { get; set; }
}

public class testeServices
{
    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private string jsonFile
    {
        get {return Path.Combine(desktopPath, "vscode", "JSONserialize", "teste", "Teste.json");}
    }

    public IEnumerable<testeModels>? GetTeste()
    {
        using (var readFile = File.OpenRead(jsonFile))
        {
            return JsonSerializer.Deserialize<testeModels[]>(readFile, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public void AddTeste(string id, double value)
    {
        testeModels x = new testeModels();
        x.id = id;
        x.value = value;

        using(var writeFile = File.OpenWrite(jsonFile))
        {
            JsonSerializer.Serialize<IE>(writeFile, x);
        }

    }
}

class ProgramSpace
{
    static void Main(string[] args)
    {
        testeServices serviço = new testeServices();
        foreach(var teste in serviço.GetTeste())
        {
            Console.WriteLine(teste.id);
            Console.WriteLine(teste.value);
        }
    }
}