using MCRental_Models;
using MCRental_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Client.ViewModels
{
    public class KlantenViewModel
    {
        private readonly KlantService _klantService;
        public List<Klant> Steden { get; set; }
        public KlantenViewModel(KlantService klantService)
        {
            _klantService = klantService;
            Steden = _klantService.getAllKlanten();
        }
    }
}
