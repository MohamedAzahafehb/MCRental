using MCRental_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MCRentalWeb.Controllers
{
    public class ReservatiesController : Controller
    {
        private readonly MCRentalDBContext _context;

        public ReservatiesController(MCRentalDBContext context)
        {
            _context = context;
        }

        public IActionResult ReservatieBeheer()
        {
            var reservaties = _context.Reservaties
                .Include(r => r.Auto)
                .Include(r => r.Gebruiker)
                .ToList();
            return View(reservaties);
        }

        // GET: Reservaties
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mCRentalDBContext = _context.Reservaties
                .Where(r => r.GebruikerId == userId)
                .Include(r => r.Auto)
                .Include(r => r.Gebruiker);
            return View(await mCRentalDBContext.ToListAsync());
        }

        // GET: Reservaties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservaties
                .Include(r => r.Auto)
                .Include(r => r.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservatie == null)
            {
                return NotFound();
            }

            return View(reservatie);
        }

        // GET: Reservaties/Create
        public IActionResult Create()
        {
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Merk");
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "Id", "Id");
            return View();
        }

        // POST: Reservaties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDatum,EindDatum,GebruikerId,AutoId,Aanmaking,Annulatie")] Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservatie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Merk", reservatie.AutoId);
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "Id", "Id", reservatie.GebruikerId);
            return View(reservatie);
        }

        // GET: Reservaties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservaties.FindAsync(id);
            if (reservatie == null)
            {
                return NotFound();
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Merk", reservatie.AutoId);
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "Id", "Id", reservatie.GebruikerId);
            return View(reservatie);
        }

        // POST: Reservaties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDatum,EindDatum,GebruikerId,AutoId,Aanmaking,Annulatie")] Reservatie reservatie)
        {
            if (id != reservatie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservatie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservatieExists(reservatie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Merk", reservatie.AutoId);
            ViewData["GebruikerId"] = new SelectList(_context.Gebruikers, "Id", "Id", reservatie.GebruikerId);
            return View(reservatie);
        }

        // GET: Reservaties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservaties
                .Include(r => r.Auto)
                .Include(r => r.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservatie == null)
            {
                return NotFound();
            }

            return View(reservatie);
        }

        // POST: Reservaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservatie = await _context.Reservaties.FindAsync(id);
            if (reservatie != null)
            {
                _context.Reservaties.Remove(reservatie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservatieExists(int id)
        {
            return _context.Reservaties.Any(e => e.Id == id);
        }
    }
}
