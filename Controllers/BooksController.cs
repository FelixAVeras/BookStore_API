using BookStoreAPI.Models;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var response = await _httpClient.GetAsync($"{urlExternalApi}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var book = await response.Content.ReadFromJsonAsync<Book>();
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return BadRequest($"Error fetching book with ID {id} from external API");
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook([FromBody] Book newBook)
    {
        var response = await _httpClient.PostAsJsonAsync(urlExternalApi, newBook);

        if (response.IsSuccessStatusCode)
        {
            var createdBook = await response.Content.ReadFromJsonAsync<Book>();
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook?.Id }, createdBook);
        }

        return BadRequest("Error creating book in external API");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var response = await _httpClient.PutAsJsonAsync($"{urlExternalApi}/{id}", updatedBook);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return BadRequest($"Error updating book with ID {id} in external API");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _httpClient.DeleteAsync($"{urlExternalApi}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return BadRequest($"Error deleting book with ID {id} from external API");
    }
}