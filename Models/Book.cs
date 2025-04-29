using System.Text.Json.Serialization;

namespace BookStoreAPI.Models;

public class Book 
{
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    // [JsonPropertyName("title")]
    // public string Author { get; set; } = string.Empty;
    
    [JsonPropertyName("pageCount")]
    public int PageCount { get; set; }
    
    [JsonPropertyName("publishDate")]
    public DateTime PublishedDate { get; set; }
}