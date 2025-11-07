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
using System.Windows.Shapes;

namespace MCRental_Client.Windows
{
    /// <summary>
    /// Interaction logic for FilaalDetailWin.xaml
    /// </summary>
    public partial class FilaalDetailWin : Window
    {
        private bool aanHetBewerken;
        private readonly Filiaal _filiaal;
        private readonly MCRentalDBContext _context;
        public FilaalDetailWin(Filiaal filiaal, MCRentalDBContext context)
        {
            InitializeComponent();
            _filiaal = filiaal;
            _context = context;
            aanHetBewerken = true;
            lblTitel.Text = $"{_filiaal.Naam} Bewerken";
            txtNaam.Text = _filiaal.Naam;
            txtAdres.Text = _filiaal.Adres;
            txtTelefoon.Text = _filiaal.Telefoon;
            txtEmail.Text = _filiaal.Email;

            txtNaam.IsReadOnly = true;
            txtAdres.IsReadOnly = true;
            txtTelefoon.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            cmbPlaats.IsEnabled = false;

            cmbPlaats.SelectedValuePath = "Id";
            cmbPlaats.ItemsSource = _context.Steden.Take(20).ToList();
            cmbPlaats.SelectedValue = _filiaal.StadId;


        }

        public FilaalDetailWin(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            _filiaal = new Filiaal();
            aanHetBewerken = false;
            lblTitel.Text = "Nieuw Filiaal Aanmaken";
            btnBewerk.Visibility = Visibility.Collapsed;
            cmbPlaats.SelectedValuePath = "Id";
            cmbPlaats.ItemsSource = _context.Steden.Take(20).ToList();
        }

        private void btnBewerk_Click(object sender, RoutedEventArgs e)
        {
            txtNaam.IsReadOnly = false;
            txtAdres.IsReadOnly = false;
            txtTelefoon.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            cmbPlaats.IsEnabled = true;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            _filiaal.Naam = txtNaam.Text;
            _filiaal.Adres = txtAdres.Text;
            _filiaal.Telefoon = txtTelefoon.Text;
            _filiaal.Email = txtEmail.Text;
            _filiaal.StadId = (int)cmbPlaats.SelectedValue;
            if (aanHetBewerken)
            {
                _context.Filialen.Update(_filiaal);
            } else
            {
                _context.Filialen.Add(_filiaal);
            }
            _context.SaveChanges();
            this.Close();
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
