using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCRental_Models;

namespace MCRentalWeb.Controllers
{
    public class FilialenController : Controller
    {
        private readonly MCRentalDBContext _context;

        public FilialenController(MCRentalDBContext context)
        {
            _context = context;
        }

        // GET: Filialen
        public async Task<IActionResult> Index()
        {
            var mCRentalDBContext = _context.Filialen.Include(f => f.Stad);
            return View(await mCRentalDBContext.ToListAsync());
        }

        // GET: Filialen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiaal = await _context.Filialen
                .Include(f => f.Stad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filiaal == null)
            {
                return NotFound();
            }

            return View(filiaal);
        }

        // GET: Filialen/Create
        public IActionResult Create()
        {
            ViewData["StadId"] = new SelectList(_context.Steden, "Id", "Naam");
            return View();
        }

        // POST: Filialen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Adres,Telefoon,Email,StadId")] Filiaal filiaal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filiaal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StadId"] = new SelectList(_context.Steden, "Id", "Naam", filiaal.StadId);
            return View(filiaal);
        }

        // GET: Filialen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiaal = await _context.Filialen.FindAsync(id);
            if (filiaal == null)
            {
                return NotFound();
            }
            ViewData["StadId"] = new SelectList(_context.Steden, "Id", "Naam", filiaal.StadId);
            return View(filiaal);
        }

        // POST: Filialen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Adres,Telefoon,Email,StadId")] Filiaal filiaal)
        {
            if (id != filiaal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filiaal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiliaalExists(filiaal.Id))
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
            ViewData["StadId"] = new SelectList(_context.Steden, "Id", "Naam", filiaal.StadId);
            return View(filiaal);
        }

        // GET: Filialen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiaal = await _context.Filialen
                .Include(f => f.Stad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filiaal == null)
            {
                return NotFound();
            }

            return View(filiaal);
        }

        // POST: Filialen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filiaal = await _context.Filialen.FindAsync(id);
            if (filiaal != null)
            {
                _context.Filialen.Remove(filiaal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiliaalExists(int id)
        {
            return _context.Filialen.Any(e => e.Id == id);
        }
    }
}
