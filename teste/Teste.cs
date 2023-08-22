using System.Text.Json;
using System.Text.Json.Serialization;

namespace teste.model;
public class TesteModelo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }

    public override string ToString() => JsonSerializer.Serialize<TesteModelo>(this);
}