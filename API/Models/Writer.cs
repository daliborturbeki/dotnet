namespace API.Models;

public class Writer {

  public int Id { get; set; }
  public string? Name { get; set; }
  public List<Book?> Books;
}
