using System.Text.Json;
using teste.model;

namespace ProgramSpace;

public class TesteModeloServices
{
    public string JsonFile => Path.Combine("Data/Teste.json");

    public List<TesteModelo> GetModels()
    {
        using (var readFile = File.OpenRead(JsonFile))
        {
            return JsonSerializer.Deserialize<List<TesteModelo>>(readFile, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public TesteModelo GetOneModel(string id)
    {
        return GetModels().FirstOrDefault(x => x.Id == id);
    }

    public void AddTesteModelo(string id, double value)
    {
        List<TesteModelo> TestModelList = GetModels();
        
        if(TestModelList.FirstOrDefault(x => x.Id == id, null) is null)
        {
            TestModelList.Add(new TesteModelo() { Id = id, Value = value });
        }
        else{
            throw new Exception("Element already exist!");
        }

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
        var product = testeModelosList.FirstOrDefault(product => product.Id == id, null);
        if (product is null) throw new NullReferenceException("Element doesnt exist");

        testeModelosList.Remove(product);

        /*
            Em uma aplicação com um banco de dados convecional, essa abordagem não é a ideial,
            mas como aqui estou utilizando um arquivo de texto, atende.
        */
        File.WriteAllText(JsonFile, "");

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
        List<TesteModelo> lista = service.GetModels();
        service.RemoveTestModel("teste3", lista);

        Console.WriteLine(service.GetOneModel("Thiago").ToString());

        foreach (var itens in lista)
        {
            Console.WriteLine(itens.Id);
            Console.WriteLine(itens.Value);
        }
    }
}