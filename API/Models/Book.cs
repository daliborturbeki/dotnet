namespace API.Models;

public class Book {

  public int Id { get; set; }
  public string? Name { get; set; }
  public int WriterId { get; set; }
  public int Pages { get; set; }
  public bool IsEBook { get; set; }

}
