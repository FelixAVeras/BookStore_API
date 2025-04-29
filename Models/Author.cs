using System.Text.Json.Serialization;

namespace BookStoreAPI.Models;

public class Author
{
    public int Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
    
    [JsonPropertyName("idBook")]
    public int IdBook { get; set; }
}