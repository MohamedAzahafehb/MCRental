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
using System.Windows.Shapes;
using MCRental_Models;

namespace MCRental_Client.Views
{
    /// <summary>
    /// Interaction logic for StedenView.xaml
    /// </summary>
    public partial class StedenView : Page
    {
        public StedenView(MCRentalDBContext context)
        {
            InitializeComponent();
            dgSteden.ItemsSource = (from stad in context.Steden
                                    select new Stad
                                    {
                                        Id = stad.Id,
                                        Naam = stad.Naam,
                                        Postcode = stad.Postcode
                                    })
                                    .ToList();
        }
    }
}
