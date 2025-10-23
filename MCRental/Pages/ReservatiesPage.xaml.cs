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
using MCRental_Client.Windows;
using MCRental_Models;

namespace MCRental_Client.Pages
{
    public partial class ReservatiesPage : Page
    {
        private List <Reservatie> reservaties = new List<Reservatie>();
        private readonly MCRentalDBContext _context;
        public ReservatiesPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            reservaties = (from reservatie in context.Reservaties
                           select new Reservatie
                           {
                               Id = reservatie.Id,
                               AutoId = reservatie.AutoId,
                               KlantId = reservatie.KlantId,
                               StartDatum = reservatie.StartDatum,
                               EindDatum = reservatie.EindDatum,
                           })
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
