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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for GebruikersPage.xaml
    /// </summary>
    public partial class GebruikersPage : Page
    {
        private List<Gebruiker> gebruikers;
        private readonly MCRentalDBContext _context;
        public GebruikersPage(MCRentalDBContext context)
        {
            InitializeComponent();
            gebruikers = (from gebruiker in context.Gebruikers
                          select new Gebruiker
                          {
                              Id = gebruiker.Id,
                              Naam = gebruiker.Naam,
                              UserName = gebruiker.UserName,
                              Email = gebruiker.Email
                          })
                          .ToList();
            dgGebruikers.ItemsSource = gebruikers;
        }
    }
}
