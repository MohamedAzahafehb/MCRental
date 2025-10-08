using MCRental_Models;
using MCRental_Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Services
{
    public class AutoService
    {
        private readonly MCRentalDBContext _context;
        public AutoService(MCRentalDBContext context)
        {
            _context = context;
        }
        public List<Auto> getAllAutos()
        {
            return _context.Autos.ToList();
        }
    }
}
