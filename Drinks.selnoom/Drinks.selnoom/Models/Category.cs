using System.Text.Json.Serialization;

namespace Drinks.selnoom.Models;

public class Category
{
    [JsonPropertyName("strCategory")]
    public string Name { get; set; }
}

public class CategoryResponse
{
    [JsonPropertyName("drinks")]
    public List<Category> Categories { get; set; }
}