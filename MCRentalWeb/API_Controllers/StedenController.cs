using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MCRental_Models;

namespace MCRentalWeb.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StedenController : ControllerBase
    {
        private readonly MCRentalDBContext _context;

        public StedenController(MCRentalDBContext context)
        {
            _context = context;
        }

        // GET: api/Steden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stad>>> GetSteden()
        {
            return await _context.Steden.ToListAsync();
        }

        // GET: api/Steden/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stad>> GetStad(int id)
        {
            var stad = await _context.Steden.FindAsync(id);

            if (stad == null)
            {
                return NotFound();
            }

            return stad;
        }

        // PUT: api/Steden/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStad(int id, Stad stad)
        {
            if (id != stad.Id)
            {
                return BadRequest();
            }

            _context.Entry(stad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StadExists(id))
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

        // POST: api/Steden
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stad>> PostStad(Stad stad)
        {
            _context.Steden.Add(stad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStad", new { id = stad.Id }, stad);
        }

        // DELETE: api/Steden/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStad(int id)
        {
            var stad = await _context.Steden.FindAsync(id);
            if (stad == null)
            {
                return NotFound();
            }

            _context.Steden.Remove(stad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StadExists(int id)
        {
            return _context.Steden.Any(e => e.Id == id);
        }
    }
}
