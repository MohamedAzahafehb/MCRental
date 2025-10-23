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

namespace MCRental_Client.Views
{
    /// <summary>
    /// Interaction logic for FilialenView.xaml
    /// </summary>
    public partial class FilialenView : Page
    {
        public FilialenView(MCRentalDBContext context)
        {
            InitializeComponent();
            dgFilialen.ItemsSource = (from filiaal in context.Filialen
                                     select new MCRental_Models.Filiaal
                                     {
                                         Id = filiaal.Id,
                                         Naam = filiaal.Naam,
                                         Adres = filiaal.Adres,
                                         Telefoon = filiaal.Telefoon,
                                         Email = filiaal.Email,
                                         StadId = filiaal.StadId
                                     })
                                     .ToList();
        }
    }
}
