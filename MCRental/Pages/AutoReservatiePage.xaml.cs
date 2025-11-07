using MCRental_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Net.Mime.MediaTypeNames;

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for AutoReservatiePage.xaml
    /// </summary>
    public partial class AutoReservatiePage : Page
    {
        private Auto _auto;
        private Page _page;
        private readonly MCRentalDBContext _context;
        private readonly Reservatie _reservatie;
        public AutoReservatiePage(Auto auto, MCRentalDBContext context, Reservatie reservatie, Page page)
        {
            Debug.WriteLine("Navigated to AutoReservatiePage");
            InitializeComponent();
            _context = context;
            _auto = auto;
            _reservatie = reservatie;
            _page = page;

            Gebruiker klant = _context.Gebruikers.FirstOrDefault(g => g.Id == _reservatie.GebruikerId);

            txtStartdatum.Text = _reservatie.StartDatum.ToShortDateString();
            txtEinddatum.Text = _reservatie.EindDatum.ToShortDateString();
            txtKlant.Text = $" {klant.Achternaam} {klant.Voornaam}";

            var autos = _context.Autos.ToList();
            cmbAutos.ItemsSource = autos;
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            if (_page.GetType().Name == "AutoDetailPage")
            {
                NavigationService.GoBack();
            }
            else if (_page.GetType().Name == "ReservatieBeheerPage")
            {
                Window.GetWindow(this).Close();
            }
        }

        private void btnKoppel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _reservatie.AutoId = _auto.Id;
                _context.Reservaties.Update(_reservatie);
                _context.SaveChanges();
                MessageBox.Show("De reservatie is succesvol gekoppeld aan de nieuwe auto.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                if (_page.GetType().Name == "AutoDetailPage")
                {
                    NavigationService.GoBack();
                }
                else if (_page.GetType().Name == "ReservatieBeheerPage")
                {
                    Window.GetWindow(this).Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbAutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _auto = (Auto)cmbAutos.SelectedItem;
        }
    }
}
