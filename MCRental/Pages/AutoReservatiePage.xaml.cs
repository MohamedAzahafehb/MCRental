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
        private readonly Auto _auto;
        private readonly MCRentalDBContext _context;
        private readonly Reservatie _reservatie;
        public AutoReservatiePage(Auto auto, MCRentalDBContext context, Reservatie reservatie)
        {
            Debug.WriteLine("Navigated to AutoReservatiePage");
            InitializeComponent();
            _context = context;
            _auto = auto;
            _reservatie = reservatie;

            txtStartdatum.Text = _reservatie.StartDatum.ToShortDateString();
            txtEinddatum.Text = _reservatie.EindDatum.ToShortDateString();
            //txtKlant.Text = $"{_reservatie.Gebruiker.Achternaam} {_reservatie.Gebruiker.Voornaam}";

            // beschikbareAutos zijn alle autos die tussen _reservatie.Startdatum en _reservatie.Einddatum niet gereserveerd zijn
            // (dus gekoppeld met andere reservaties in deze periode) en ze moeten beschikbaar zijn
            //List<Auto> beschikbareAutos = _context.Autos
            //    .Where(a => a.Beschikbaar &&
            //            !_context.Reservaties.Any(r =>
            //            r.AutoId == a.Id &&
            //            r.Id != _reservatie.Id && // zodat de huidige reservatie genegeerd wordt
            //            r.StartDatum < _reservatie.EindDatum &&
            //            r.EindDatum > _reservatie.StartDatum))
            //    .ToList();
            //var test = _context.Reservaties
            //    .Where(r =>
            //        r.StartDatum < _reservatie.EindDatum &&
            //        r.EindDatum > _reservatie.StartDatum)
            //    .Select(r => new { r.Id, r.AutoId, r.StartDatum, r.EindDatum })
            //    .ToList();


            //foreach (var res in test)
            //{
            //    Debug.WriteLine($"Conflict: Auto {res.AutoId} - {res.StartDatum:d} to {res.EindDatum:d}");
            //}

            var autos = _context.Autos.ToList();
            cmbAutos.ItemsSource = autos;
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
