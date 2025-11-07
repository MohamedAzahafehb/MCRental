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
    /// Interaction logic for AutoDetailPage.xaml
    /// </summary>
    public partial class AutoDetailPage : Page
    {
        private readonly Auto _auto;
        private readonly MCRentalDBContext _context;
        public List<Auto> Autos { get; set; }
        public List<Reservatie> Reservaties { get; set; }
        public AutoDetailPage(Auto auto, MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _auto = auto;
            SetAutoDetails();
        }

        private void SetAutoDetails()
        {
            txtMerk.Text = _auto.Merk;
            txtMerk.IsReadOnly = true;
            txtModel.Text = _auto.Model;
            txtModel.IsReadOnly = true;
            txtNummerplaat.Text = _auto.Nummerplaat;
            txtNummerplaat.IsReadOnly = true;
            txtDagprijs.Text = _auto.DagPrijs.ToString("C");
            txtDagprijs.IsReadOnly = true;
            lblStatus.Content = _auto.Beschikbaar ? "actief" : "inactief";
            txtType.Text = _auto.type;
            txtType.IsReadOnly = true;
            txtFiliaal.Text = _auto.FiliaalId.ToString();
            txtFiliaal.IsReadOnly = true;
            btnDeactiveer.IsEnabled = true;
        }


        private void btnBewerk_Click(object sender, RoutedEventArgs e)
        {
            btnDeactiveer.IsEnabled = false;
            txtMerk.IsReadOnly = false;
            txtModel.IsReadOnly = false;
            txtNummerplaat.IsReadOnly = false;
            txtDagprijs.Text = _auto.DagPrijs.ToString();
            txtDagprijs.IsReadOnly = false;
            txtType.IsReadOnly = false;
            txtFiliaal.IsReadOnly = false;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            _auto.Merk = txtMerk.Text;
            _auto.Model = txtModel.Text;
            _auto.Nummerplaat = txtNummerplaat.Text;
            if (double.TryParse(txtDagprijs.Text, out double dagprijs))
            {
                _auto.DagPrijs = dagprijs;
            }
            else
            {
                MessageBox.Show("Ongeldige dagprijs. Voer een geldig getal in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _auto.type = txtType.Text;

            _context.Autos.Update(_auto);
            _context.SaveChanges();
            Window.GetWindow(this).Close();
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            SetAutoDetails();
        }

        private void btnDeactiveer_Click(object sender, RoutedEventArgs e)
        {
            Reservaties = _context.Reservaties.Where(r => r.AutoId == _auto.Id).ToList();

            if (Reservaties.Count > 0)
            {
                spNieuweKoppeling.Visibility = Visibility.Visible;
                btnDeactiveer.IsEnabled = false;
                btnSaveKoppeling.IsEnabled = true;
                dgReservaties.DataContext = this;
                dgReservaties.ItemsSource = Reservaties;
                Reservatie eersteReservatie = Reservaties.First();
            }
            else
            {
                _auto.Beschikbaar = false;
                _context.Autos.Update(_auto);
                _context.SaveChanges();
                MessageBox.Show("De auto is succesvol gedeactiveerd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                Window.GetWindow(this).Close();

            }
        }

        private void btnKoppel_Click(object sender, RoutedEventArgs e)
        {
            var reservatie = (sender as Button).DataContext as Reservatie;
            NavigationService.Navigate(new AutoReservatiePage(_auto, _context, reservatie));
        }

        private void btnSaveKoppeling_Click(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
            MessageBox.Show("reservaties zijn bijgewerkt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
