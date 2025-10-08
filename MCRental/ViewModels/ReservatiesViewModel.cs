using MCRental_Models;
using MCRental_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Client.ViewModels
{
    public class ReservatiesViewModel
    {
        private readonly ReservatieService _reservatieService;
        public List<Reservatie> Steden { get; set; }
        public ReservatiesViewModel(ReservatieService reservatieService)
        {
            _reservatieService = reservatieService;
            Steden = _reservatieService.getAllReservaties();
        }
    }
}
