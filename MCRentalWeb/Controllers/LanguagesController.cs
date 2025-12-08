using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MCRental_Models;

namespace MCRentalWeb.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly MCRentalDBContext _context;

        public LanguagesController(MCRentalDBContext context)
        {
            _context = context;
        }

        public IActionResult ChangeLanguage(string code, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(code)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}
