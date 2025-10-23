using MCRental_Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ReservatieDetailsWin.xaml
    /// </summary>
    public partial class ReservatieDetailsWin : Window
    {
        private readonly MCRentalDBContext _context;
        private readonly Reservatie _reservatie;
        public ReservatieDetailsWin(Reservatie reservatie, MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _reservatie = reservatie;

            LaadReservatieDetails();
        }

        private void LaadReservatieDetails()
        {
            try
            {
                var res = _context.Reservaties
                    .Include(r => r.Auto)
                        .ThenInclude(a => a.Filiaal)
                            .ThenInclude(f => f.Stad)
                    //.Include(r => r.Klant)
                    .FirstOrDefault(r => r.Id == _reservatie.Id);

                if (res == null)
                {
                    MessageBox.Show("Reservatie niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }

                lblStartdatum.Content = res.StartDatum.ToShortDateString();
                lblEinddatum.Content = res.EindDatum.ToShortDateString();

                if (res.Auto != null)
                {
                    lblMerk.Content = res.Auto.Merk;
                    lblModel.Content = res.Auto.Model;
                    lblNummerplaat.Content = res.Auto.Nummerplaat;
                    lblType.Content = res.Auto.type;
                    lblDagprijs.Content = $"{res.Auto.DagPrijs:C}";
                }

                if (res.Auto?.Filiaal != null)
                {
                    var f = res.Auto.Filiaal;
                    lblFiliaalNaam.Content = f.Naam;
                    lblAdres.Content = f.Adres;
                    lblTelefoon.Content = f.Telefoon;
                    lblEmail.Content = f.Email;

                    if (f.Stad != null)
                    {
                        lblStad.Content = f.Stad.Naam;
                        lblPostcode.Content = f.Stad.Postcode;
                    }
                }

                //if (res.Klant != null)
                //{
                //    lblKlantNaam.Content = $"{res.Klant.Voornaam} {res.Klant.Achternaam}";
                //    lblKlantEmail.Content = res.Klant.Email;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($" fout bij het opladen van de details:\n{ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
