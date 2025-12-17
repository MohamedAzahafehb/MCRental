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
    public class ReservatiesController : ControllerBase
    {
        private readonly MCRentalDBContext _context;

        public ReservatiesController(MCRentalDBContext context)
        {
            _context = context;
        }

        // GET: api/Reservaties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservatie>>> GetReservaties()
        {
            return await _context.Reservaties.ToListAsync();
        }

        // GET: api/Reservaties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservatie>> GetReservatie(int id)
        {
            var reservatie = await _context.Reservaties.FindAsync(id);

            if (reservatie == null)
            {
                return NotFound();
            }

            return reservatie;
        }

        // PUT: api/Reservaties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservatie(int id, Reservatie reservatie)
        {
            if (id != reservatie.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservatieExists(id))
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

        // POST: api/Reservaties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservatie>> PostReservatie(Reservatie reservatie)
        {
            _context.Reservaties.Add(reservatie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservatie", new { id = reservatie.Id }, reservatie);
        }

        // DELETE: api/Reservaties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservatie(int id)
        {
            var reservatie = await _context.Reservaties.FindAsync(id);
            if (reservatie == null)
            {
                return NotFound();
            }

            _context.Reservaties.Remove(reservatie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservatieExists(int id)
        {
            return _context.Reservaties.Any(e => e.Id == id);
        }
    }
}
