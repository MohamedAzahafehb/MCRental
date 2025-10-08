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
using MCRental_Client.ViewModels;

namespace MCRental_Client.Views
{
    /// <summary>
    /// Interaction logic for StedenView.xaml
    /// </summary>
    public partial class StedenView : Page
    {
        public StedenView()
        {
            InitializeComponent();
            DataContext = new StedenViewModel(new MCRental_Services.StadService(new MCRental_Persistence.MCRentalDBContext()));
        }
    }
}
