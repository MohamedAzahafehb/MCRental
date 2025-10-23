using MCRental_Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCRental_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MCRentalDBContext _context;
        public MainWindow(MCRentalDBContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private void Autos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.AutosView(_context));
        }

        private void Filialen_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.FilialenView(_context));
        }

        private void Steden_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.StedenView(_context));

        }

        private void Klanten_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new Views.KlantenView(_context));
        }

        private void Reservaties_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.ReservatiesView(_context));
        }
    }
}