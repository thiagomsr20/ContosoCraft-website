using System.Text.Json;
using System.Text.Json.Serialization;

namespace teste.model;
public class testeModelo
{
    [JsonPropertyName("id")]
    public string id { get; set; }

    [JsonPropertyName("value")]
    public double value { get; set; }

    public override string ToString() => JsonSerializer.Serialize<testeModelo>(this);
}