using MCRental_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for VoegAutoToe.xaml
    /// </summary>
    public partial class VoegAutoToe : Window
    {
        private readonly MCRentalDBContext _context;
        public VoegAutoToe(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;

            cmbFiliaal.ItemsSource = _context.Filialen.ToList();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (string.IsNullOrWhiteSpace(txtMerk.Text) ||
                        string.IsNullOrWhiteSpace(txtModel.Text) ||
                        string.IsNullOrWhiteSpace(txtNummerplaat.Text) ||
                        string.IsNullOrWhiteSpace(txtDagprijs.Text) ||
                        string.IsNullOrWhiteSpace(txtType.Text) ||
                        cmbFiliaal.SelectedItem == null)
                {
                    throw new Exception("Vul alle verplichte velden in.");
                }

                if (!double.TryParse(txtDagprijs.Text, out double dagprijs))
                {
                    throw new Exception("Ongeldige dagprijs. Voer een geldig getal in.");
                }

                Filiaal geselecteerdFiliaal = (Filiaal)cmbFiliaal.SelectedItem;
                if (geselecteerdFiliaal == null)
                {
                    throw new Exception("Selecteer een geldig filiaal.");
                }

                _context.Autos.Add(new Auto
                {
                    Merk = txtMerk.Text,
                    Model = txtModel.Text,
                    Nummerplaat = txtNummerplaat.Text,
                    DagPrijs = double.Parse(txtDagprijs.Text),
                    Beschikbaar = rbnBeschikbaar.IsChecked ?? false,
                    type = txtType.Text,
                    FiliaalId = geselecteerdFiliaal.Id
                });
                MessageBox.Show("Auto toegevoegd!");
                _context.SaveChanges();
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show("Fout bij het toevoegen van de auto: " + ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
