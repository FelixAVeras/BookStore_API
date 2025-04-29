using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public string urlExternalApi = "https://fakerestapi.azurewebsites.net/api/v1/Authors";
    
    public AuthorsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors() 
    {
        var response = await _httpClient.GetAsync(urlExternalApi);

        if (response.IsSuccessStatusCode)
        {
            var authors = await response.Content.ReadAsStringAsync();

            return Content(authors, "application/json");
        }

        return BadRequest("Error fetching data from external API");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        var response = await _httpClient.GetAsync($"{urlExternalApi}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var authors = await response.Content.ReadFromJsonAsync<Book>();
            if (authors == null)
            {
                return NotFound();
            }
            return Ok(authors);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return BadRequest($"Error fetching authors with ID {id} from external API");
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthors(Author newAuthor)
    {
      var response = await _httpClient.PostAsJsonAsync(urlExternalApi, newAuthor);

      if (response.IsSuccessStatusCode)
      {
        var createdAuthor = await response.Content.ReadFromJsonAsync<Author>();
          
        return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor?.Id }, createdAuthor);
      }

      return BadRequest("Error creating author in external API");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, Author updatedAuthor)
    {
      var response = await _httpClient.PutAsJsonAsync($"{urlExternalApi}/{id}", updatedAuthor);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      
      if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        return NotFound();
      }

      return BadRequest($"Error updating author with ID {id} in external API");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
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

      return BadRequest($"Error deleting author with ID {id} from external API");
    }
}