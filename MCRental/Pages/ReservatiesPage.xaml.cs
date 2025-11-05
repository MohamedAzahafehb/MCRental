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
        // extra velden in tabel: datum aanmaking, datum annulatie - done
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
            reservaties = _context.Reservaties
                .Where(r => r.GebruikerId == _gebruiker.Id)
                .OrderByDescending(r => r.StartDatum)
                .ToList();
            //(from reservatie in _context.Reservaties
            //           select new Reservatie
            //           {
            //               Id = reservatie.Id,
            //               AutoId = reservatie.AutoId,
            //               GebruikerId = reservatie.GebruikerId,
            //               StartDatum = reservatie.StartDatum,
            //               EindDatum = reservatie.EindDatum,
            //           })
            //           .Where(r => r.GebruikerId == _gebruiker.Id)
            //           .OrderByDescending(r => r.StartDatum)
            //           .ToList();

            dgReservaties.ItemsSource = reservaties;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Reservatie res = (sender as Button).DataContext as Reservatie;
            new ReservatieDetailsWin(res, _context).ShowDialog();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSort = (cmbSort.SelectedItem as ComboBoxItem).Content as string;
            switch (selectedSort)
            {
                case "Nieuwste":
                    dgReservaties.ItemsSource = reservaties.OrderByDescending(r => r.StartDatum).ToList();
                    break;
                case "Oudste":
                    dgReservaties.ItemsSource = reservaties.OrderBy(r => r.StartDatum).ToList();
                    break;
                default:
                    dgReservaties.ItemsSource = reservaties;
                    break;
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFilter = (cmbFilter.SelectedItem as ComboBoxItem).Content as string;
            switch (selectedFilter)
            {
                case "Aankomend":
                    dgReservaties.ItemsSource = reservaties.Where(r => r.StartDatum >= DateTime.Now && r.Annulatie == null).ToList();
                    break;
                case "Verlopen":
                    dgReservaties.ItemsSource = reservaties.Where(r => r.EindDatum < DateTime.Now && r.Annulatie == null).ToList();
                    break;
                case "Geannuleerd":
                    dgReservaties.ItemsSource = reservaties.Where(r => r.Annulatie != null).ToList();
                    break;
                default:
                    dgReservaties.ItemsSource = reservaties;
                    break;
            }
        }
    }
}
