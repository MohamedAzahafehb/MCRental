using MCRental_Client.Windows;
using MCRental_Models;
using Microsoft.AspNetCore.Identity;
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
    /// Interaction logic for GebruikersPage.xaml
    /// </summary>
    public partial class GebruikersPage : Page
    {
        // Gebruiker details window: rollen aanpassen
        private List<Gebruiker> gebruikers;
        private readonly MCRentalDBContext _context;
        UserManager<Gebruiker> _userManager;
        public GebruikersPage(MCRentalDBContext context, UserManager<Gebruiker> userManager)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;
            gebruikers = _context.Gebruikers
                .Include(g => g.Stad)
                .ToList();
            dgGebruikers.ItemsSource = gebruikers;
        }

        public void btnBewerk_Click(object sender, RoutedEventArgs e)
        {
            Gebruiker gebruiker = (sender as Button).DataContext as Gebruiker;
            new GebruikerDetaliWin(gebruiker, _context, _userManager).ShowDialog();
        }
    }
}
