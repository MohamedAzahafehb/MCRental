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
    /// Interaction logic for GebruikerDetaliWin.xaml
    /// </summary>
    public partial class GebruikerDetaliWin : Window
    {
        private readonly Gebruiker _gebruiker;
        private readonly MCRentalDBContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        public GebruikerDetaliWin(Gebruiker gebruiker, MCRentalDBContext context, UserManager<Gebruiker> userManager)
        {
            InitializeComponent();
            _gebruiker = gebruiker;
            _context = context;
            _userManager = userManager;

            txtVoornaam.Text = _gebruiker.Voornaam;
            txtAchternaam.Text = _gebruiker.Achternaam;
            txtGebruikersnaam.Text = _gebruiker.UserName;
            txtGeboortedatum.Text = _gebruiker.GeboorteDatum.ToShortDateString();
            txtAdres.Text = _gebruiker.Adres;
            txtEmail.Text = _gebruiker.Email;
            txtPlaats.Text = $"{_gebruiker.Stad?.Postcode} {_gebruiker.Stad?.Naam}";

            List<ListBoxItem> roles = new List<ListBoxItem>();
            List<string> userRoles = (from ur in _context.UserRoles
                                      where ur.UserId == _gebruiker.Id
                                      select ur.RoleId)
                                     .ToList();
            foreach (IdentityRole role in _context.Roles)
            {
                bool isSelected = userRoles.Contains(role.Id);
                roles.Add(new ListBoxItem { Content = role.Id, IsSelected = isSelected });
            }
            lbRollen.ItemsSource = roles;
            lbRollen.Visibility = Visibility.Visible;


        }

        private async void lbRollen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ListBoxItem item in lbRollen.Items)
            {
                string role = item.Content.ToString();
                if (lbRollen.SelectedItems.Contains(item))
                    await _userManager.AddToRoleAsync(_gebruiker, role);
                else
                    await _userManager.RemoveFromRoleAsync(_gebruiker, role);
            }
        }
    }
}
