using Restaurant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy OknoKelner.xaml
    /// </summary>
    public partial class OknoKelner : Window
    {
        Restauracja restauracja;
        public OknoKelner(Restauracja restauracja, EnumUprawienia uprawienia)
        {
            InitializeComponent();
            this.restauracja = restauracja;
            cmbStatus.ItemsSource = Enum.GetNames(typeof(EnumStanZamowienia));
            lstZamowienia.ItemsSource = new ObservableCollection<KeyValuePair<string, Zamowienie>>(restauracja.Zamowienia);
        }
        private void EscapeButtonClicked(object sender, RoutedEventArgs e)
        {

        }

    }

}


