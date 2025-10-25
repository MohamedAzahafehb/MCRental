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
        public LogIn(UserManager<Gebruiker> userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
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
                            App.MainWindow.lblGebruiker.Content = $"Ingelogd als: {user.Voornaam} {user.Achternaam}";
                            MessageBox.Show($"Welkom {user.Voornaam} {user.Achternaam}!", "Inloggen gelukt", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
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
    }
}
