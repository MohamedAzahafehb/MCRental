using MCRental_Models;
using Microsoft.AspNetCore.Authorization;
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
    public class AutosController : Controller
    {
        private readonly MCRentalDBContext _context;

        public AutosController(MCRentalDBContext context)
        {
            _context = context;
        }

        public IActionResult Overzicht(int filiaalId, DateTime pickupDate, DateTime returnDate)
        {
            // Validatie
            if (filiaalId <= 0 || pickupDate <= DateTime.Now)
            {
                TempData["Error"] = "Ongeldige zoekparameters";
                return RedirectToAction("Index", "Home");
            }

            // Haal beschikbare auto's op
            var beschikbareAutos = _context.Autos
                .Where(a => a.FiliaalId == filiaalId)
                .ToList();

            ViewBag.Filiaal = _context.Filialen
                .Include(f => f.Stad)
                .FirstOrDefault(f => f.Id == filiaalId);
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;

            return View(beschikbareAutos);
        }

        public IActionResult Beheer()
        {
            var autos = _context.Autos
                .Include(a => a.Filiaal)
                .ToList();
            return View(autos);
        }

        public IActionResult Reserveer(int autoId, DateTime pickupDate, DateTime returnDate)
        {
            var auto = _context.Autos
                .Include(a => a.Filiaal)
                    .ThenInclude(f => f.Stad)
                .FirstOrDefault(a => a.Id == autoId);
            if (auto == null)
            {
                TempData["Error"] = "Auto niet gevonden";
                return RedirectToAction("Overzicht", "Autos");
            }
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;
            ViewBag.Filiaal = auto.Filiaal;
            return View(auto);
        }

        // GET: Autos
        public async Task<IActionResult> Index()
        {
            var mCRentalDBContext = _context.Autos.Include(a => a.Filiaal);
            return View(await mCRentalDBContext.ToListAsync());
        }

        // GET: Autos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos
                .Include(a => a.Filiaal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // Bevestig reservatie - WEL [Authorize] hier!
        [HttpPost]
        [Authorize] // Dit zorgt voor redirect naar login als niet ingelogd
        public IActionResult BevestigReservatie(int autoId, DateTime pickupDate, DateTime returnDate)
        {
            try
            {
                // Haal ingelogde gebruiker ID op
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    // Dit zou niet moeten gebeuren door [Authorize], maar veiligheidscheck
                    return RedirectToAction("Login", "Account", new
                    {
                        returnUrl = Url.Action("Reserveer", new { autoId, pickupDate, returnDate })
                    });
                }

                var reservatie = new Reservatie
                {
                    AutoId = autoId,
                    GebruikerId = userId,
                    StartDatum = pickupDate,
                    EindDatum = returnDate,
                    Aanmaking = DateTime.Now
                };

                _context.Reservaties.Add(reservatie);
                _context.SaveChanges();

                TempData["Success"] = "Reservatie succesvol aangemaakt!";
                return RedirectToAction("Bevestiging", new { id = reservatie.Id });
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in BevestigReservatie: {ex.Message}");

                TempData["Error"] = "Er ging iets fout bij het maken van de reservatie. Probeer opnieuw.";
                return RedirectToAction("Reserveer", new { autoId, pickupDate, returnDate });
            }
        }

        // Bevestigingspagina
        [Authorize]
        public IActionResult Bevestiging(int id)
        {
            var reservatie = _context.Reservaties
                .Include(r => r.Auto)
                    .ThenInclude(a => a.Filiaal)
                        .ThenInclude(f => f.Stad)
                .Include(r => r.Gebruiker)
                .FirstOrDefault(r => r.Id == id);

            if (reservatie == null)
            {
                TempData["Error"] = "Reservatie niet gevonden";
                return RedirectToAction("Index", "Home");
            }

            // Check of deze reservatie van de ingelogde gebruiker is
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (reservatie.GebruikerId != userId)
            {
                TempData["Error"] = "Je hebt geen toegang tot deze reservatie";
                return RedirectToAction("Index", "Home");
            }

            return View(reservatie);
        }

        // GET: Autos/Create
        public IActionResult Create()
        {
            ViewData["FiliaalId"] = new SelectList(_context.Filialen, "Id", "Adres");
            return View();
        }

        // POST: Autos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Merk,Model,Nummerplaat,DagPrijs,Beschikbaar,type,FiliaalId")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FiliaalId"] = new SelectList(_context.Filialen, "Id", "Adres", auto.FiliaalId);
            return View(auto);
        }

        // GET: Autos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            ViewData["FiliaalId"] = new SelectList(_context.Filialen, "Id", "Adres", auto.FiliaalId);
            return View(auto);
        }

        // POST: Autos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Merk,Model,Nummerplaat,DagPrijs,Beschikbaar,type,FiliaalId")] Auto auto)
        {
            if (id != auto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.Id))
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
            ViewData["FiliaalId"] = new SelectList(_context.Filialen, "Id", "Adres", auto.FiliaalId);
            return View(auto);
        }

        // GET: Autos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos
                .Include(a => a.Filiaal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
            if (auto != null)
            {
                _context.Autos.Remove(auto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
            return _context.Autos.Any(e => e.Id == id);
        }
    }
}
