using MCRental_Models;
using MCRentalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCRentalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MCRentalDBContext _context;

        public HomeController(ILogger<HomeController> logger, MCRentalDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Filialen = _context.Filialen
            .OrderBy(f => f.Naam)
            .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
