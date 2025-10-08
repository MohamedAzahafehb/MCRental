using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCRental_Models;
using MCRental_Services;

namespace MCRental_Client.ViewModels
{
    public class StedenViewModel
    {
        private readonly StadService _stadService;
        public List<Stad> Steden { get; set; }
        public StedenViewModel(StadService stadService)
        {
            _stadService = stadService;
            Steden = _stadService.getAllSteden();
        }

    }
}
