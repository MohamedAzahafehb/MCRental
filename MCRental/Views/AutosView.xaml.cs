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
        public AutosView()
        {
            InitializeComponent();
            DataContext = new ViewModels.AutosViewModel(new MCRental_Services.AutoService(new MCRental_Persistence.MCRentalDBContext()));
        }
    }
}
