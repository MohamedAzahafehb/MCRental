using MCRental_Client.Windows;
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

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for FilialenPage.xaml
    /// </summary>
    public partial class FilialenPage : Page
    {
        // Filailen details window
        // rol == Admin? alle velden kunnen aangepast worden en opgeslagen
        // rol != Admin? alleen details bekijken
        private readonly MCRentalDBContext _context;
        public FilialenPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            RefreshFilialen();
        }

        public void RefreshFilialen()
        {
            var filialen = _context.Filialen
                            .ToList();
            dgFilialen.ItemsSource = filialen;
        }

        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            Filiaal filiaal = (sender as Button).DataContext as Filiaal;
            new FilaalDetailWin(filiaal, _context).ShowDialog();
            RefreshFilialen();
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            new FilaalDetailWin(_context).ShowDialog();
            RefreshFilialen();
        }
    }
}
