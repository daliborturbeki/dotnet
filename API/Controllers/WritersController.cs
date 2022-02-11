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
  public class WritersController : ControllerBase {
    private readonly IWriterService ws;

    public WritersController(IWriterService writerService) {
      ws = writerService;
    }

    // GET: api/Writers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Writer>>> GetWriters() {
      return Ok(await ws.All());
    }

    // GET: api/Writers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Writer>> GetWriter(int id) {
      var writer = await ws.Get(id);
      if (writer == null) {
        return NotFound();
      }
      return writer;
    }

    // PUT: api/Writers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWriter(int id, Writer writer) {
      if (id != writer.Id) {
        return BadRequest();
      }
      if (await ws.Update(writer)) {
        return NoContent();
      }
      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    // POST: api/Writers
    [HttpPost]
    public async Task<ActionResult<Book>> PostWriter(Writer writer) {
      await ws.Insert(writer);
      return CreatedAtAction("GetWriter", new { id = writer.Id }, writer);
    }

    // DELETE: api/Writers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWriter(int? id) {
      if (id == null) return NotFound();

      Writer writer;
      if ((writer = await ws.Get(id)) != null) {
        await ws.Delete(writer.Id);
        return NoContent();
      } else {
        return NotFound();
      }
    }
  }
}
