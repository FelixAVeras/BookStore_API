using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public string urlExternalApi = "https://fakerestapi.azurewebsites.net/api/v1/Books";
    
    public BooksController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks() 
    {
        var response = await _httpClient.GetAsync(urlExternalApi);

        if (response.IsSuccessStatusCode)
        {
            var books = await response.Content.ReadAsStringAsync();

            return Content(books, "application/json");
        }

        return BadRequest("Error fetching data from external API");
    }

    
}