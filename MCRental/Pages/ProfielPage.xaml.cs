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
    /// Interaction logic for ProfielPage.xaml
    /// </summary>
    public partial class ProfielPage : Page
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly MCRentalDBContext _context;
        private Gebruiker _gebruiker;
        public ProfielPage(UserManager<Gebruiker> userManager, MCRentalDBContext context)
        {
            _userManager = userManager;
            _context = context;
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Haal de huidige ingelogde gebruiker op
                Gebruiker currentUser = App.Gebruiker;
                if (currentUser == null)
                {
                    MessageBox.Show("Geen gebruiker gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Laad de gebruiker inclusief Stad
                _gebruiker = _context.Users
                    .Include(g => g.Stad)
                    .FirstOrDefault(g => g.Id == currentUser.Id);

                if (_gebruiker == null)
                {
                    MessageBox.Show("Gebruiker niet gevonden in de database.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Bind alle steden aan de combobox
                cmbStad.ItemsSource = _context.Steden.ToList();
                cmbStad.DisplayMemberPath = "Naam";
                cmbStad.SelectedValuePath = "Id";

                // Vul velden in
                txtVoornaam.Text = _gebruiker.Voornaam;
                txtAchternaam.Text = _gebruiker.Achternaam;
                dpGeboortedatum.SelectedDate = _gebruiker.GeboorteDatum;
                txtEmail.Text = _gebruiker.Email;
                txtAdres.Text = _gebruiker.Adres;
                txtUserName.Text = _gebruiker.UserName;
                txtPhoneNumber.Text = _gebruiker.PhoneNumber;
                cmbStad.SelectedValue = _gebruiker.StadId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij laden profiel: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _gebruiker.Email = txtEmail.Text;
                _gebruiker.Adres = txtAdres.Text;
                _gebruiker.UserName = txtUserName.Text;
                _gebruiker.PhoneNumber = txtPhoneNumber.Text;

                if (cmbStad.SelectedValue is int stadId)
                    _gebruiker.StadId = stadId;

                _context.Update(_gebruiker);
                _context.SaveChanges();

                MessageBox.Show("Wijzigingen succesvol opgeslagen!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij opslaan: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Annuleer_Click(object sender, RoutedEventArgs e)
        {
            // Herlaad originele data
            Page_Loaded(sender, e);
        }
    }
}
