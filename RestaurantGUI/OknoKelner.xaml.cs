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
        EnumUprawienia uprawienia;
        public OknoKelner(Restauracja restauracja, EnumUprawienia uprawienia)
        {
            InitializeComponent();
            this.restauracja = restauracja;
            this.uprawienia = uprawienia;
            cmbStatus.ItemsSource = Enum.GetNames(typeof(EnumStanZamowienia));
            lstZamowienia.ItemsSource = new ObservableCollection<Zamowienie>(restauracja.Zamowienia.Values);
        }

        private void ChangeButtonClicked(object sender, RoutedEventArgs e)
        {
            if (lstZamowienia.SelectedItem is Zamowienie wybraneZamowienie)
            {
                restauracja.ZmienStatusZamowienia(wybraneZamowienie.IdZamowienia, (EnumStanZamowienia)cmbStatus.SelectedIndex);
            }
            lstZamowienia.ItemsSource = new ObservableCollection<Zamowienie>(restauracja.Zamowienia.Values);
        }

        private void ZmienionoZamowienie(object sender, SelectionChangedEventArgs e)
        {
            if (lstZamowienia.SelectedItem is Zamowienie wybraneZamowienie)
            {
                cmbStatus.SelectedIndex = (int)wybraneZamowienie.StanZamowienia;
            }
        }

        public void ZapisClicked(object sender, RoutedEventArgs e)
        {
            restauracja.ZapiszXML("restauracja.xml");
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            DodajZamowienie dodawanieZamowienia = new DodajZamowienie(restauracja, uprawienia, "");
            dodawanieZamowienia.Show();
            this.Close();
        }

        private void EscapeButtonClicked(object sender, RoutedEventArgs e)
        {
            if (uprawienia.Equals(EnumUprawienia.pracownik))
            {
                MainWindow okno = new MainWindow();
                okno.Show();
                this.Close();
            }
            else
            {
                Admin oknoAdmina = new Admin(restauracja, "pracownicy");
                oknoAdmina.Show();
                this.Close();
            }
        }

    }

}


