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
    public class FilialenController : ControllerBase
    {
        private readonly MCRentalDBContext _context;

        public FilialenController(MCRentalDBContext context)
        {
            _context = context;
        }

        // GET: api/Filialen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filiaal>>> GetFilialen()
        {
            return await _context.Filialen.ToListAsync();
        }

        // GET: api/Filialen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filiaal>> GetFiliaal(int id)
        {
            var filiaal = await _context.Filialen.FindAsync(id);

            if (filiaal == null)
            {
                return NotFound();
            }

            return filiaal;
        }

        // PUT: api/Filialen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiliaal(int id, Filiaal filiaal)
        {
            if (id != filiaal.Id)
            {
                return BadRequest();
            }

            _context.Entry(filiaal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FiliaalExists(id))
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

        // POST: api/Filialen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filiaal>> PostFiliaal(Filiaal filiaal)
        {
            _context.Filialen.Add(filiaal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiliaal", new { id = filiaal.Id }, filiaal);
        }

        // DELETE: api/Filialen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiliaal(int id)
        {
            var filiaal = await _context.Filialen.FindAsync(id);
            if (filiaal == null)
            {
                return NotFound();
            }

            _context.Filialen.Remove(filiaal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FiliaalExists(int id)
        {
            return _context.Filialen.Any(e => e.Id == id);
        }
    }
}
