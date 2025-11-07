using MCRental_Client.Pages;
using MCRental_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly MCRentalDBContext _context;
        public LogIn(UserManager<Gebruiker> userManager, MCRentalDBContext context)
        {
            _context = context;
            _userManager = userManager;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!tbPassword.Password.IsNullOrEmpty() && !tbUsername.Text.IsNullOrEmpty())
                {
                    Gebruiker? user = await _userManager.FindByNameAsync(tbUsername.Text);
                    if (user != null)
                    {
                        bool succeeded = await _userManager.CheckPasswordAsync(user, tbPassword.Password);
                        if (succeeded)
                        {
                            {
                                App.Gebruiker = user;
                                // visibility weg van login en register knop
                                App.MainWindow.btnLogin.Visibility = Visibility.Collapsed;
                                Close();
                            }
                            IdentityUserRole<string>? userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == App.Gebruiker.Id);

                            MessageBox.Show(userRole.RoleId);
                            if (userRole != null)
                            {
                                if (userRole.RoleId == "Admin")
                                {
                                    App.MainWindow.frmMain.Navigate(new AutoBeheerPage(_context));
                                    App.MainWindow.mniAutobeheer.Visibility = Visibility.Visible;
                                    App.MainWindow.mniReservatiebeheer.Visibility = Visibility.Visible;
                                    App.MainWindow.mniGebruikers.Visibility = Visibility.Visible;
                                    App.MainWindow.mniFilialenbeheer.Visibility = Visibility.Visible;
                                }
                                else if (userRole.RoleId == "Klant")
                                {
                                    App.MainWindow.mniAuto.Visibility = Visibility.Visible;
                                    App.MainWindow.mniReservaties.Visibility = Visibility.Visible;
                                }
                                App.MainWindow.btnLogout.Visibility = Visibility.Visible;
                                App.MainWindow.mniProfiel.Visibility = Visibility.Visible;
                            }
                        }
                        tbError.Text = "Ongeldige username of wachtwoord.";
                    }
                    else
                    {
                        tbError.Text = "Je moet een username en wachtwoord invullen.";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fout bij inloggen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

    }
}
