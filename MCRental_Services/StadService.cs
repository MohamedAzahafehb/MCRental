using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCRental_Models;
using MCRental_Persistence;

namespace MCRental_Services
{
    public class StadService
    {
        private readonly MCRentalDBContext _context;
        public StadService(MCRentalDBContext context)
        {
            _context = context;
        }
        public List<Stad> getAllSteden()
        {
            return _context.Steden.ToList();
        }
    }
}
