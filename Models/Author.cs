namespace BookStoreAPI.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    // public DateTime DateOfBirth { get; set; }
    // public DateTime DateOfDeath { get; set; }
    // public List<Book> Books { get; set; } = new List<Book>();
}