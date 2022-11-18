using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMasivAPI.Model;

namespace TestMasivAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Perssons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersson()
        {
            return await _context.Persson
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        // GET: api/Perssons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersson(long id)
        {
            var persson = await _context.Persson.FindAsync(id);

            if (persson == null)
            {
                return NotFound();
            }

            return persson;
        }

        // PUT: api/Perssons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersson(long id, Person persson)
        {
            if (id != persson.Id)
            {
                return BadRequest();
            }

            _context.Entry(persson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerssonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Perssons
        [HttpPost]
        public async Task<ActionResult<Person>> PostPersson(Person persson)
        {
            _context.Persson.Add(persson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersson", new { id = persson.Id }, persson);
        }

        // DELETE: api/Perssons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersson(long id)
        {
            var persson = await _context.Persson.FindAsync(id);
            if (persson == null)
            {
                return NotFound();
            }

            // _context.Persson.Remove(persson);

            // It is not recomended to delete elements from storage
            // Change an Active state instead
            persson.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerssonExists(long id)
        {
            return _context.Persson.Any(e => e.Id == id);
        }
    }
}
