using MCRental_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCRental_Client.Views
{
    /// <summary>
    /// Interaction logic for AutosView.xaml
    /// </summary>
    public partial class AutosView : Page
    {
        public AutosView(MCRentalDBContext context)
        {
            InitializeComponent();
            dgAutos.ItemsSource = (from auto in context.Autos
                                   select new MCRental_Models.Auto
                                   {
                                       Id = auto.Id,
                                       Merk = auto.Merk,
                                       Model = auto.Model,
                                       Nummerplaat = auto.Nummerplaat,
                                       DagPrijs = auto.DagPrijs,
                                       Beschikbaar = auto.Beschikbaar,
                                       type = auto.type,
                                        FiliaalId = auto.FiliaalId
                                   })
                                   .ToList();
        }
    }
}
