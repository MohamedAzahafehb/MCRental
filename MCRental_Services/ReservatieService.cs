using MCRental_Models;
using MCRental_Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Services
{
    public class ReservatieService
    {
        private readonly MCRentalDBContext _context;
        public ReservatieService(MCRentalDBContext context)
        {
            _context = context;
        }
        public List<Reservatie> getAllReservaties()
        {
            return _context.Reservaties.ToList();
        }
    }
}
