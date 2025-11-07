using MCRental_Client.Windows;
using MCRental_Models;
using Microsoft.AspNetCore.Identity;
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
    /// Interaction logic for HomeKlantPage.xaml
    /// </summary>
    public partial class HomeKlantPage : Page
    {
        private readonly MCRentalDBContext _context;
        private readonly UserManager<Gebruiker> _userManager;

        public HomeKlantPage(UserManager<Gebruiker> userManager, MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;

            cmbFilialen.ItemsSource = _context.Filialen.ToList();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            // check ingevulde velden (selected filiaal, datums)
            // open AutoOverzichtPage met de juiste parameters
            var startDatum = dpStartDatum.SelectedDate;
            var eindDatum = dpEindDatum.SelectedDate;
            var geselecteerdFiliaal = cmbFilialen.SelectedItem as Filiaal;
            if (startDatum == null || eindDatum == null || geselecteerdFiliaal == null)
            {
                MessageBox.Show("Gelieve alle velden in te vullen.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigationService.Navigate(new AutoOverzichtPage(_userManager, _context, geselecteerdFiliaal, startDatum.Value, eindDatum.Value));
        }
    }
}
