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
    /// Interaction logic for AutoDetailWin.xaml
    /// </summary>
    public partial class AutoDetailWin : Window
    {
        private readonly Auto _auto;
        public AutoDetailWin(Auto auto)
        {
            InitializeComponent();
            _auto = auto;
            LaadAutoDetails();
        }

        private void LaadAutoDetails()
        {
            lblMerk.Content = _auto.Merk;
            lblModel.Content = _auto.Model;
            lblNummerplaat.Content = _auto.Nummerplaat;
            lblDagprijs.Content = _auto.DagPrijs.ToString("C");
            lblBeschikbaar.Content = _auto.Beschikbaar ? "Ja" : "Nee";
            lblType.Content = _auto.type;
            lblFiliaal.Content = _auto.FiliaalId.ToString();
        }
    }
}
