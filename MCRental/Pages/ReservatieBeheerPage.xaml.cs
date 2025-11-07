using MCRental_Client.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for ReservatieBeheerPage.xaml
    /// </summary>
    /// 
    public partial class ReservatieBeheerPage : Page
    {
        // btn ReservatieDetails
        // nieuw window: reservatie koppelen aan een andere auto, reservatie annuleren
        private List<Reservatie> reservaties = new List<Reservatie>();
        private readonly MCRentalDBContext _context;
        public ReservatieBeheerPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;

            RefreshReservaties();
        }

        private void RefreshReservaties()
        {
            reservaties = _context.Reservaties
                           .ToList();

            dgReservaties.ItemsSource = reservaties;
        }

        public void btnKoppel_Click(object sender, RoutedEventArgs e)
        {
            var reservatie = (sender as Button).DataContext as Reservatie;
            var window = new AutoDetailWin();
            window.Show();
            window.frmMain.Navigate(new AutoReservatiePage(reservatie.Auto, _context, reservatie, this));
            RefreshReservaties();
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
    }
}
