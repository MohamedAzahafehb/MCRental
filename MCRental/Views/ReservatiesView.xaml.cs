using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MCRental_Models;
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
    /// Interaction logic for ReservatiesView.xaml
    /// </summary>
    public partial class ReservatiesView : Page
    {
        public ReservatiesView(MCRentalDBContext context)
        {
            InitializeComponent();
            dgReservaties.ItemsSource = (from reservatie in context.Reservaties
                                         select new MCRental_Models.Reservatie
                                         {
                                             Id = reservatie.Id,
                                             KlantId = reservatie.KlantId,
                                             AutoId = reservatie.AutoId,
                                             StartDatum = reservatie.StartDatum,
                                             EindDatum = reservatie.EindDatum
                                         })
                                         .ToList();
        }
    }
}
