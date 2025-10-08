using MCRental_Models;
using MCRental_Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Services
{
    public class FiliaalService
    {
        private readonly MCRentalDBContext _context;
        public FiliaalService(MCRentalDBContext context)
        {
            _context = context;
        }
        public List<Filiaal> getAllFilialen()
        {
            return _context.Filialen.ToList();
        }
    }
}
