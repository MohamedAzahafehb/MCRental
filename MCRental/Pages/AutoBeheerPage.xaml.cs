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

namespace MCRental_Client.Pages
{
    /// <summary>
    /// Interaction logic for AutoBeheerPage.xaml
    /// </summary>
    public partial class AutoBeheerPage : Page
    {
        private List<string> selectedTypes = new List<string>();
        private List<Auto> autos = new List<Auto>();
        public AutoBeheerPage(MCRentalDBContext context)
        {
            InitializeComponent();
            autos = (from auto in context.Autos
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
                                   .ToList();
            dgAutos.ItemsSource = autos;

            var typesDistinct = autos.Select(a => a.type).Distinct().ToList();

            cmbTypes.ItemsSource = typesDistinct;
        }
        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            Auto auto = (sender as Button).DataContext as Auto;
            new AutoDetailWin(auto).ShowDialog();
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
