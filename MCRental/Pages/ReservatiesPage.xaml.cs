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
    public partial class ReservatiesPage : Page
    {
        //TODO:
        //Reservaties:
        //sorteren op Nieuwste of Oudste (startdatum) - todo
        // filteren op aankomende, verlopen, geannuleerde - todo
        // Details:
        //reservatie annuleren - todo
        // evt: data aanpassen, een beetje ingewikkeld, checken op ebschikbaarheid - todo
        //Validatie
        //annuleren kan enkel als startdatum in de toekomst ligt - todo
        //aanpassen kan enkel als startdatum in de toekomst ligt - todo

        private List <Reservatie> reservaties = new List<Reservatie>();
        private readonly MCRentalDBContext _context;
        private Gebruiker _gebruiker;
        public ReservatiesPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _gebruiker = App.Gebruiker;
            reservaties = (from reservatie in context.Reservaties
                           select new Reservatie
                           {
                               Id = reservatie.Id,
                               AutoId = reservatie.AutoId,
                               GebruikerId = reservatie.GebruikerId,
                               StartDatum = reservatie.StartDatum,
                               EindDatum = reservatie.EindDatum,
                           })
                           .Where(r => r.GebruikerId == _gebruiker.Id)
                           .ToList();

            dgReservaties.ItemsSource = reservaties;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Reservatie res = (sender as Button).DataContext as Reservatie;
            new ReservatieDetailsWin(res, _context).ShowDialog();
        }

    }
}
