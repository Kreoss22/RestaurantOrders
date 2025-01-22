using Restaurant;
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

namespace RestaurantGUI
{
    /// <summary>
    /// Logika interakcji dla klasy admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        Restauracja restauracja;
        Konto kontoAdmina;
        string obecnaTabela;

        public Admin()
        {
            InitializeComponent();
        }

    }
}
