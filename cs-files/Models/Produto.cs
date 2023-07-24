using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.Models;

public class Product
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("maker")]
    public string Maker { get; set; } = string.Empty;

    [JsonPropertyName("img")]
    public string Imagem { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    public int[] Rating { get; set; }

    /* 
        Polimorfismo no método ToString para tornar os objetos no formato JSON
        Reescrever o método ToString é importante para retornar representações
        mais legíveis de objetos
        Nesse caso, será serializando os objetos para o JSON
        Mas poderia ser uma simples string de retorno, por exemplo:
            
            public override string ToString() => $"Id : {id}; Maker: {Maker}"
    */
    public override string ToString() => JsonSerializer.Serialize<Product>(this);

}