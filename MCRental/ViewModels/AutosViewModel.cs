using MCRental_Models;
using MCRental_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Client.ViewModels
{
    public class AutosViewModel
    {
        private readonly AutoService _autoService;
        public List<Auto> Steden { get; set; }
        public AutosViewModel(AutoService autoService)
        {
            _autoService = autoService;
            Steden = _autoService.getAllAutos();
        }
    }
}
