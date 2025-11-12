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
using System.Windows.Shapes;

namespace MCRental_Client.Windows
{
    /// <summary>
    /// Interaction logic for Registreer.xaml
    /// </summary>
    public partial class Registreer : Window
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly MCRentalDBContext _context;

        public Registreer(UserManager<Gebruiker> userManager, MCRentalDBContext context)
        {
            InitializeComponent();
            _userManager = userManager;
            _context = context;

            LaadSteden();
        }

        private void LaadSteden()
        {
            cmbStad.ItemsSource = _context.Steden.OrderBy(s => s.Naam).Take(20).ToList();
        }

        private async void btnRegistreer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtVoornaam.Text) ||
                    string.IsNullOrWhiteSpace(txtAchternaam.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtAdres.Text) ||
                    string.IsNullOrWhiteSpace(txtUserName.Text) ||
                    string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                    dpGeboortedatum.SelectedDate == null ||
                    cmbStad.SelectedItem == null)
                {
                    throw new Exception("Gelieve alle velden in te vullen!");
                }
                
                var gebruiker = new Gebruiker
                {
                    Voornaam = txtVoornaam.Text,
                    Achternaam = txtAchternaam.Text,
                    GeboorteDatum = dpGeboortedatum.SelectedDate ?? DateTime.Now,
                    Email = txtEmail.Text,
                    Adres = txtAdres.Text,
                    StadId = (int)cmbStad.SelectedValue,
                    UserName = txtUserName.Text,
                    PhoneNumber = txtPhoneNumber.Text
                };

                var result = await _userManager.CreateAsync(gebruiker, pwdWachtwoord.Password);

                if (result.Succeeded)
                {
                    App.Gebruiker = gebruiker;
                    MessageBox.Show("Registratie geslaagd!" + App.Gebruiker.UserName, "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    _context.Add(new IdentityUserRole<string>
                    {
                        UserId = gebruiker.Id,
                        RoleId = "Klant"
                    });
                    _context.SaveChanges();

                    App.MainWindow.btnLogin.Visibility = Visibility.Collapsed;
                    App.MainWindow.btnRegistreer.Visibility = Visibility.Collapsed;
                    App.MainWindow.mniAuto.Visibility = Visibility.Visible;
                    App.MainWindow.mniReservaties.Visibility = Visibility.Visible;
                    App.MainWindow.btnLogout.Visibility = Visibility.Visible;
                    App.MainWindow.mniProfiel.Visibility = Visibility.Visible;

                    this.Close();
                }
                else
                {
                    throw new Exception(string.Join("\n", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}");
            }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
