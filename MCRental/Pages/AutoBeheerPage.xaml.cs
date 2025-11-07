using MCRental_Client.Windows;
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
using System.Collections.ObjectModel;

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for AutoBeheerPage.xaml
    /// </summary>
    public partial class AutoBeheerPage : Page
    {
        /*Wanneer op btn Bewerk:
        zoekbalk, zoekt in Merk, Model, Nummerplaat
        filteren op types, beschiknaarheid
        datagrid observable maken: automatisch updaten bij bewerken
         */
        private List<string> selectedTypes = new List<string>();
        private List<Auto> autos = new List<Auto>();
        private readonly MCRentalDBContext _context;
        public AutoBeheerPage(MCRentalDBContext context)
        {
            _context = context;
            InitializeComponent();
            RefreshAutos();
        }

        public void RefreshAutos()
        {
            autos = _context.Autos.ToList();
            dgAutos.ItemsSource = autos;

            var typesDistinct = autos.Select(a => a.type).Distinct().ToList();

            cmbTypes.ItemsSource = typesDistinct;
        }
        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            Auto auto = (sender as Button).DataContext as Auto;
            new AutoDetailWin(auto, _context).ShowDialog();
            RefreshAutos();
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
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new VoegAutoToe(_context).ShowDialog();
            RefreshAutos();
        }

        private void cmbSort_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
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
                default:
                    dgAutos.ItemsSource = autos;
                    break;
            }
        }
    }
}
