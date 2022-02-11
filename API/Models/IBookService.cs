using System.Collections;

namespace API.Models;

public interface IBookService {
  Task<IEnumerable> All();
  Task<IEnumerable> AllEBooks();

  Task<Book?> Get(int? id);

  Task<int> Insert(Book? book);

  Task<bool> Update(Book? book);

  Task<int> Delete(int id);
}
