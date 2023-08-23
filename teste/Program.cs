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

        // Limpar todo conteúdo do database antes de escreve um novo formatado
        // Não é uma boa ideia esse método, pesqueisar outro
        // Mas serviu para identificar que o problema é que ele sobreescreve
        // o que já existe
        File.WriteAllText(JsonFile, "");

        // Tentar usar métodos LINQ para remover informações específicas

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
        service.RemoveTestModel("Marlon", lista);
        service.AddTesteModelo("wer2", 351);
        Console.WriteLine(service.GetOneModel("Thiago").ToString());

        foreach (var itens in lista)
        {
            Console.WriteLine(itens.Id);
            Console.WriteLine(itens.Value);
        }
    }
}