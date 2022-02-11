using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class BookService : IBookService {
  public BookService(WebAPIContext context) {
    Db = context;
  }

  public async Task<IEnumerable> All() {
    return await Db.Books.ToListAsync();
  }
  public async Task<IEnumerable> AllEBooks() {
    var eBooks =
      (from book in Db.Books
      where book.IsEBook == true
      select book);

    return await eBooks.ToListAsync();
  }

  public async Task<Book?> Get(int? id) {
    return await Db.Books.FirstOrDefaultAsync(m => m.Id == id);
  }

  public async Task<int> Insert(Book? book) {
    if (book != null) {
      Db.Add(book);
      return await Db.SaveChangesAsync();
    }
    return -1;
  }

  public async Task<bool> Update(Book? book) {
    if (book != null) {
      try {
        Db.Update(book);
        await Db.SaveChangesAsync();
        return true;
      } catch (DbUpdateConcurrencyException) {
        return false;
      }
    }
    return false;
  }

  public async Task<int> Delete(int id) {
    var book = await Get(id);
    if (book != null) {
      Db.Books.Remove(book);
      return await Db.SaveChangesAsync();
    }
    return 0;
  }

  public WebAPIContext Db { get; }
}
