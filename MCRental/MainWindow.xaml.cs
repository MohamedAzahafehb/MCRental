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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Autos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.AutosView());
        }

        private void Filialen_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.FilialenView());
        }

        private void Steden_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.StedenView());
        }

        private void Klanten_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.KlantenView());
        }

        private void Reservaties_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.ReservatiesView());
        }
    }
}