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
using System.Windows.Shapes;

namespace MCRental_Client.Windows
{
    /// <summary>
    /// Interaction logic for AutoDetailWin.xaml
    /// </summary>
    public partial class AutoDetailWin : Window
    {
        /*
         * prijs aanpassen, merk, model, nummerplaat, type. zonder problemen
        maar wanneer beschikbaarheid op false wordt gezet wordt er eerst alle reservaties die aan de auto gekoppeld zijn weergegeven,
        moeten die reservaties aan andere autos gekoppeld worden die op die datum beschikbaarzijn(datum).
        dan pas kan de auto gedeactiveerd worden
        */
        
        public AutoDetailWin(Auto auto, MCRentalDBContext context)
        {
            InitializeComponent();
            frmMain.Navigate(new Pages.AutoDetailPage(auto, context));
        }

    }
}
