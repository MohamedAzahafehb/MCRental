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
    /// Interaction logic for KlantenView.xaml
    /// </summary>
    public partial class KlantenView : Page
    {
        public KlantenView()
        {
            InitializeComponent();
            DataContext = new ViewModels.KlantenViewModel(new MCRental_Services.KlantService(new MCRental_Persistence.MCRentalDBContext()));
        }
    }
}
