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
    /// Interaction logic for FilialenPage.xaml
    /// </summary>
    public partial class FilialenPage : Page
    {
        // Filailen details window
        // rol == Admin? alle velden kunnen aangepast worden en opgeslagen
        // rol != Admin? alleen details bekijken
        private readonly MCRentalDBContext _context;
        private List<Filiaal> filialen;
        public FilialenPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            RefreshFilialen();
        }

        public void RefreshFilialen()
        {
            filialen = _context.Filialen
                            .ToList();
            dgFilialen.ItemsSource = filialen;
        }

        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            Filiaal filiaal = (sender as Button).DataContext as Filiaal;
            new FilaalDetailWin(filiaal, _context).ShowDialog();
            RefreshFilialen();
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            new FilaalDetailWin(_context).ShowDialog();
            RefreshFilialen();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSort = (cmbSort.SelectedItem as ComboBoxItem).Content as string;
            switch (selectedSort)
            {
                case "Naam oplopend":
                    dgFilialen.ItemsSource = filialen.OrderBy(a => a.Naam).ToList();
                    break;
                case "Naam aflopend":
                    dgFilialen.ItemsSource = filialen.OrderByDescending(a => a.Naam).ToList();
                    break;
                default:
                    dgFilialen.ItemsSource = filialen;
                    break;
            }
        }
    }
}
