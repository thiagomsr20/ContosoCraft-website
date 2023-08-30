﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.Models;

public class Product
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("maker")]
    public string? Maker { get; set; }

    [JsonPropertyName("img")]
    public string? Image { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
    public List<int> Ratings { get; set; } = new();

    public override string ToString() => JsonSerializer.Serialize<Product>(this);
}