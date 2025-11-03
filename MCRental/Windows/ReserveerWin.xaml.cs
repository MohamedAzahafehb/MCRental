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
    /// Interaction logic for ReserveerWin.xaml
    /// </summary>
    public partial class ReserveerWin : Window
    {
        Auto _auto;
        MCRentalDBContext _context;
        Filiaal filiaal;
        DateTime startDatum;
        DateTime eindDatum;
        public ReserveerWin(MCRentalDBContext context, Auto auto, Gebruiker gebruiker, Filiaal filiaal, DateTime startDatum, DateTime eindDatum)
        {
            InitializeComponent();
            _context = context;
            _auto = auto;
            this.filiaal = filiaal;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;
            DataContext = _auto;

            lblStartDatum.Content = startDatum.ToShortDateString();
            lblEindDatum.Content = eindDatum.ToShortDateString();

            int aantalDagen = (eindDatum - startDatum).Days;
            txtTotaalPrijs.Text = $"{aantalDagen * _auto.DagPrijs:C2} voor {aantalDagen} dagen.";
        }

        private void btnBevestig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reservatie = new Reservatie
                {
                    AutoId = _auto.Id,
                    GebruikerId = App.Gebruiker.Id,
                    StartDatum = startDatum,
                    EindDatum = eindDatum
                };
                _context.Add(reservatie);
                _context.SaveChanges();
                MessageBox.Show("Reservatie succesvol aangemaakt!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het aanmaken van de reservatie: " + ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
