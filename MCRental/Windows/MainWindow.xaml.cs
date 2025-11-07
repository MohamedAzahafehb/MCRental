using MCRental_Client.Pages;
using MCRental_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MCRentalDBContext _context;
        private UserManager<Gebruiker> _userManager;
        public MainWindow(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _userManager = App.ServiceProvider.GetRequiredService<UserManager<Gebruiker>>();
            frmMain.Navigate(new HomeKlantPage(_userManager, _context));

            // deze twee buttons zijn enkel om het testen vlot te laten verlopen
            btnAdmin.Visibility = Visibility.Collapsed;
            btnKlant.Visibility = Visibility.Collapsed;
        }

        private void mniAuto_Click(object sender, RoutedEventArgs e)
        {
            //initieel openbaar
            // user nodig om te reserveren
            frmMain.Navigate(new HomeKlantPage(_userManager, _context));
        }

        private void mniReservaties_Click(object sender, RoutedEventArgs e)
        {
            //user nodig
            frmMain.Navigate(new ReservatiesPage(_context));
        }

        private void mniAutobeheer_Click(object sender, RoutedEventArgs e)
        {
            // role = admin vereist
            frmMain.Navigate(new AutoBeheerPage(_context));
        }

        private void mniReservatiebeheer_Click(object sender, RoutedEventArgs e)
        {
            // role = admin vereist
            frmMain.Navigate(new ReservatieBeheerPage(_context));
        }

        private void mniGebruikers_Click(object sender, RoutedEventArgs e)
        {
            // role = admin vereist
            frmMain.Navigate(new GebruikersPage(_context, _userManager));
        }

        private void mniFilialenbeheer_Click(object sender, RoutedEventArgs e)
        {
            // role = admin vereist
            frmMain.Navigate(new FilialenPage(_context));
        }

        private void mniProfiel_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new ProfielPage(_userManager, _context));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            new LogIn(_userManager, _context).ShowDialog();
        }

        private void btnRegistreer_Click(object sender, RoutedEventArgs e)
        {
            new Registreer(_userManager, _context).ShowDialog();
        }

        

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.Gebruiker = null;
            btnLogout.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Visible;
            btnRegistreer.Visibility = Visibility.Visible;
            frmMain.Navigate(new HomeKlantPage(_userManager, _context));
            App.MainWindow.mniAuto.Visibility = Visibility.Collapsed;
            App.MainWindow.mniReservaties.Visibility = Visibility.Collapsed;
            App.MainWindow.mniProfiel.Visibility = Visibility.Collapsed;
            App.MainWindow.mniAutobeheer.Visibility = Visibility.Collapsed;
            App.MainWindow.mniReservatiebeheer.Visibility = Visibility.Collapsed;
            App.MainWindow.mniGebruikers.Visibility = Visibility.Collapsed;
            App.MainWindow.mniFilialenbeheer.Visibility = Visibility.Collapsed;
        }

        private async void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Gebruiker user = await _userManager.FindByNameAsync("mocromed");
            App.Gebruiker = user;

            App.MainWindow.mniAuto.Visibility = Visibility.Collapsed;
            App.MainWindow.mniReservaties.Visibility = Visibility.Collapsed;
            App.MainWindow.frmMain.Navigate(new AutoBeheerPage(_context));
            App.MainWindow.mniAutobeheer.Visibility = Visibility.Visible;
            App.MainWindow.mniReservatiebeheer.Visibility = Visibility.Visible;
            App.MainWindow.mniGebruikers.Visibility = Visibility.Visible;
            App.MainWindow.mniFilialenbeheer.Visibility = Visibility.Visible;
            App.MainWindow.mniProfiel.Visibility = Visibility.Visible;
        }

        private async void btnKlant_Click(object sender, RoutedEventArgs e)
        {
            Gebruiker user = await _userManager.FindByNameAsync("ayman");
            App.Gebruiker = user;

            App.MainWindow.mniAuto.Visibility = Visibility.Visible;
            App.MainWindow.mniReservaties.Visibility = Visibility.Visible;
            App.MainWindow.mniProfiel.Visibility = Visibility.Visible;
            App.MainWindow.mniAutobeheer.Visibility = Visibility.Collapsed;
            App.MainWindow.mniReservatiebeheer.Visibility = Visibility.Collapsed;
            App.MainWindow.mniGebruikers.Visibility = Visibility.Collapsed;
            App.MainWindow.mniFilialenbeheer.Visibility = Visibility.Collapsed;

        }
    }
}
