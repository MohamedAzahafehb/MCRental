using MCRental_Client.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for AutoOverzichtPage.xaml
    /// </summary>
    /// private List<Auto> autos = new List<Auto>();
    public partial class AutoOverzichtPage : Page
    {
        private List<string> selectedTypes = new List<string>();
        private List<Auto> autos = new List<Auto>();
        private Filiaal filiaal;
        private DateTime startDatum, eindDatum;
        private readonly MCRentalDBContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        // TODO:
        // omdat datum in Page al wordt ingegeven en niet meer in Window kan de totaalprijs berkend worden al - todo

        //Validatie:
        //auto moet beschikbaar zijn - todo
        //reserveren kan enkel als startdatum 48u in de toekomst ligt - todo
        //einddatum moet min na startdatum liggen - todo
        //reservatie mag niet overlappen met bestaande reservaties - todo
        //einddatum - startdatum altijd afronden op hele dagen - todo
        //totaalprijs = aantal dagen * dagprijs - todo
        public AutoOverzichtPage(UserManager<Gebruiker> userManager, MCRentalDBContext context, Filiaal filiaal, DateTime startDatum, DateTime eindDatum)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;
            this.filiaal = filiaal;
            this.startDatum = startDatum;
            this.eindDatum = eindDatum;

            var gereserveerdeAutoIds = (from reservatie in _context.Reservaties
                                        where !(reservatie.EindDatum <= this.startDatum || reservatie.StartDatum >= this.eindDatum)
                                        select reservatie.AutoId).Distinct().ToList();

            autos = (from auto in _context.Autos
                                   select new MCRental_Models.Auto
                                   {
                                       Id = auto.Id,
                                       Merk = auto.Merk,
                                       Model = auto.Model,
                                       Nummerplaat = auto.Nummerplaat,
                                       DagPrijs = auto.DagPrijs,
                                       Beschikbaar = auto.Beschikbaar,
                                       type = auto.type,
                                       FiliaalId = auto.FiliaalId
                                   })
                                   .Where(a => a.FiliaalId == this.filiaal.Id && !gereserveerdeAutoIds.Contains(a.Id))
                                   .OrderBy(a => a.DagPrijs)
                                   .ThenBy(a => a.Merk)
                                   .ThenBy(a => a.Model)
                                   .ToList();
            
            if(autos.Count == 0)
            {
                txtGeenAutos.Text = "Er zijn geen beschikbare auto's voor de geselecteerde datums in dit filiaal.";
            } else
            {
                dgAutos.ItemsSource = autos;
            }

            var typesDistinct = autos.Select(a => a.type).Distinct().ToList();

            cmbTypes.ItemsSource = typesDistinct;
            lblStartdt.Content = this.startDatum;
            lblEinddt.Content = this.eindDatum;
            lblFiliaal.Content = this.filiaal.Naam;
        }

        private void ReserveerButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.Gebruiker == null)
            {
                var result = MessageBox.Show("Gelieve in te loggen of te registreren om een auto te kunnen reserveren.", "Inloggen vereist", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK) {
                    new LogIn(_userManager, _context).ShowDialog();
                }
                return;
            }
            else
            {
                Auto auto = (sender as Button).DataContext as Auto;
                new ReserveerWin(_context, auto, App.Gebruiker, filiaal, startDatum, eindDatum).ShowDialog();
            }
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedSort = (cmbSort.SelectedItem as ComboBoxItem).Content as string;
            switch (selectedSort)
            {
                case "Prijs oplopend":
                    dgAutos.ItemsSource = autos.OrderBy(a => a.DagPrijs).ThenBy(a => a.Merk).ThenBy(a => a.Model).ToList();
                    break;
                case "Prijs aflopend":
                    dgAutos.ItemsSource = autos.OrderByDescending(a => a.DagPrijs).ThenBy(a => a.Merk).ThenBy(a => a.Model).ToList();
                    break;
                case "Merk A-Z":
                    dgAutos.ItemsSource = autos.OrderBy(a => a.Merk).ThenBy(a => a.Model).ThenBy(a => a.DagPrijs).ToList();
                    break;
                case "Merk Z-A":
                    dgAutos.ItemsSource = autos.OrderByDescending(a => a.Merk).ThenBy(a => a.Model).ThenBy(a => a.DagPrijs).ToList();
                    break;
                default:
                    dgAutos.ItemsSource = autos;
                    break;
            }
        }

        private void TypeCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            string type = checkBox.Tag.ToString();

            if (checkBox.IsChecked == true)
            {
                if (!selectedTypes.Contains(type))
                    selectedTypes.Add(type);
            }
            else
            {
                selectedTypes.Remove(type);
            }

            // Filter autos
            if (selectedTypes.Count == 0)
            {
                dgAutos.ItemsSource = autos;
                cmbTypes.Text = "Alle types";
            }
            else
            {
                dgAutos.ItemsSource = autos
                    .Where(a => selectedTypes.Contains(a.type))
                    .ToList();

                cmbTypes.Text = $"{selectedTypes.Count} geselecteerd";
            }
        }
    }
}
