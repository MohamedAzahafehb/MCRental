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
    /// Interaction logic for FilialenView.xaml
    /// </summary>
    public partial class FilialenView : Page
    {
        public FilialenView()
        {
            InitializeComponent();
            DataContext = new ViewModels.FilialenViewModel(new MCRental_Services.FiliaalService(new MCRental_Persistence.MCRentalDBContext()));
        }
    }
}
