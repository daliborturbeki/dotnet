using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class WriterService : IWriterService {
  public WriterService(WebAPIContext context) {
    Db = context;
  }

  public async Task<IEnumerable> All() {
    return await Db.Writers.ToListAsync();
  }

  public async Task<Writer?> Get(int? id) {
    return await Db.Writers.FirstOrDefaultAsync(m => m.Id == id);
  }

  public async Task<bool> WriterExists(int? id) {
    if(await Db.Writers.FirstOrDefaultAsync(m => m.Id == id) != null) {
      return true;
    } else {
      return false;
    }
  }

  public async Task<int> Insert(Writer? writer) {
    if (writer != null) {
      Db.Add(writer);
      return await Db.SaveChangesAsync();
    }
    return -1;
  }

  public async Task<bool> Update(Writer? writer) {
    if (writer != null) {
      try {
        Db.Update(writer);
        await Db.SaveChangesAsync();
        return true;
      } catch (DbUpdateConcurrencyException) {
        return false;
      }
    }
    return false;
  }

  public async Task<int> Delete(int id) {
    var writer = await Get(id);
    if (writer != null) {
      Db.Writers.Remove(writer);
      return await Db.SaveChangesAsync();
    }
    return 0;
  }

  public WebAPIContext Db { get; }
}
