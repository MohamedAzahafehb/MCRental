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
    /// Interaction logic for ReserveerWin.xaml
    /// </summary>
    public partial class ReserveerWin : Window
    {
        Auto _auto;
        public ReserveerWin(Auto auto)
        {
            InitializeComponent();
            _auto = auto;
            DataContext = _auto;

        }
    }
}
