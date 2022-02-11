#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase {
    private readonly IBookService bs;
    private readonly IWriterService ws;

    public BooksController(IBookService bookService, IWriterService writerService) {
      bs = bookService;
      ws = writerService;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks() {
      return Ok(await bs.All());
    }

    // GET: api/Books/Ebooks
    [HttpGet("EBooks")]
    public async Task<ActionResult<IEnumerable<Book>>> GetEBooks() {
        return Ok(await bs.AllEBooks());
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id) {
      var book = await bs.Get(id);
      if (book == null) {
        return NotFound();
      }
      return book;
    }

    // PUT: api/Books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book) {
      if (id != book.Id) {
        return BadRequest();
      }
      if (await bs.Update(book)) {
        return NoContent();
      }
      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    // POST: api/Books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book) {
      if(await ws.WriterExists(book.WriterId)) {
      await bs.Insert(book);
        return CreatedAtAction("GetBook", new { id = book.Id }, book);
      } else {
        return NotFound("Writer was not found");
      }
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int? id) {
      if (id == null) return NotFound();

      Book book;
      if ((book = await bs.Get(id)) != null) {
        await bs.Delete(book.Id);
        return NoContent();
      } else {
        return NotFound();
      }
    }
  }
}
