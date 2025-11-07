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
        public FilialenPage(MCRentalDBContext context)
        {
            InitializeComponent();
            _context = context;
            var filialen = (from filiaal in _context.Filialen
                            select new Filiaal
                            {
                                Id = filiaal.Id,
                                Naam = filiaal.Naam,
                                Adres = filiaal.Adres,
                                StadId = filiaal.StadId
                            })
                            .ToList();
            dgFilialen.ItemsSource = filialen;
        }

        private void BewerkButton_Click(object sender, RoutedEventArgs e)
        {
            //implement

        }
    }
}
