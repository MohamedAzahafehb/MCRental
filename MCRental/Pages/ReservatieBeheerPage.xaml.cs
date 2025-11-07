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
            reservaties = (from reservatie in context.Reservaties
                           select new Reservatie
                           {
                               Id = reservatie.Id,
                               AutoId = reservatie.AutoId,
                               Auto = reservatie.Auto,
                               GebruikerId = reservatie.GebruikerId,
                               Gebruiker = reservatie.Gebruiker,
                               StartDatum = reservatie.StartDatum,
                               EindDatum = reservatie.EindDatum,
                           })
                           .ToList();

            dgReservaties.ItemsSource = reservaties;

        }
    }
}
