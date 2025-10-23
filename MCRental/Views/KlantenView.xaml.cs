//using MCRental_Persistence;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace MCRental_Client.Views
//{
//    /// <summary>
//    /// Interaction logic for KlantenView.xaml
//    /// </summary>
//    public partial class KlantenView : Page
//    {
//        public KlantenView(MCRentalDBContext context)
//        {
//            InitializeComponent();
//            dgGebruikers.ItemsSource = (from gebruiker in context.Gebruikers
//                                     select new MCRental_Models.Gebruiker
//                                     {
//                                         Id = gebruiker.Id,
//                                         Voornaam = gebruiker.Voornaam,
//                                         Achternaam = gebruiker.Achternaam,
//                                         Email = gebruiker.Email,
//                                         Stad = gebruiker.Stad
//                                     })
//                                     .ToList();
//        }
//    }
//}
