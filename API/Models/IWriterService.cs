using System.Collections;

namespace API.Models;

public interface IWriterService {
  Task<IEnumerable> All();

  Task<Writer?> Get(int? id);
  Task<bool> WriterExists(int? id);

  Task<int> Insert(Writer? writer);

  Task<bool> Update(Writer? writer);

  Task<int> Delete(int id);
}
