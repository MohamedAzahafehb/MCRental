using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MCRental_Models;
using System.Threading.Tasks;

namespace MCRental.Controllers
{
    public class GebruikersController : Controller
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GebruikersController(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var gebruikers = await _userManager.Users.Include(u => u.Stad).ToListAsync();
            return View(gebruikers);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var gebruiker = await _userManager.Users.Include(u => u.Stad).FirstOrDefaultAsync(u => u.Id == id);
            if (gebruiker == null) return NotFound();

            return View(gebruiker);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var gebruiker = await _userManager.FindByIdAsync(id);
            if (gebruiker == null) return NotFound();

            return View(gebruiker);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role)) return BadRequest();

            var gebruiker = await _userManager.FindByIdAsync(id);
            if (gebruiker == null) return NotFound();

            // Remove existing roles
            var roles = await _userManager.GetRolesAsync(gebruiker);
            await _userManager.RemoveFromRolesAsync(gebruiker, roles);

            // Add selected role
            await _userManager.AddToRoleAsync(gebruiker, role);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var gebruiker = await _userManager.FindByIdAsync(id);
            if (gebruiker == null) return NotFound();

            await _userManager.DeleteAsync(gebruiker);
            return RedirectToAction(nameof(Index));
        }
    }
}