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
        public MainWindow(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void mniAuto_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new AutoOverzichtPage(_context));
        }

        private void mniReservaties_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new ReservatiesPage(_context));
        }

        private void mniAutobeheer_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new AutoBeheerPage(_context));
        }

        private void mniReservatiebeheer_Click(object sender, RoutedEventArgs e)
        {
            //frmMain.Navigate(new ReservatiesPage(_context));
        }

        private void mniGebruikers_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new GebruikersPage(_context));
        }

        private void mniFilialenbeheer_Click(object sender, RoutedEventArgs e)
        {
            //frmMain.Navigate(new FilialenPage(_context));
        }

        private void mniProfiel_Click(object sender, RoutedEventArgs e)
        {
            //frmMain.Navigate(new ProfielPage(_context));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            new LogIn(App.ServiceProvider.GetRequiredService<UserManager<Gebruiker>>()).ShowDialog();
        }

        private void btnRegistreer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
