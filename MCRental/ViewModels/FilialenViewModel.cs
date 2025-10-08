using MCRental_Models;
using MCRental_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Client.ViewModels
{
    public class FilialenViewModel
    {
        private readonly FiliaalService _filiaalService;
        public List<Filiaal> Steden { get; set; }
        public FilialenViewModel(FiliaalService filiaalService)
        {
            _filiaalService = filiaalService;
            Steden = _filiaalService.getAllFilialen();
        }
    }
}
