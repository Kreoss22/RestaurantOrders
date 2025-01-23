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
    /// Logika interakcji dla klasy admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        Restauracja restauracja;
        string obecnaTabela;

        public Admin(Restauracja restauracja, string obecnaTabela)
        {
            InitializeComponent();
            this.restauracja = restauracja;
            this.obecnaTabela = obecnaTabela;
        }

        private void inicjalizacjaTabeli()
        {
            switch (obecnaTabela)
            {
                case "pracownicy":
                    lstDane.ItemsSource = new ObservableCollection<Pracownik>(restauracja.Pracownicy);
                    break;
                case "klienci":
                    lstDane.ItemsSource = new ObservableCollection<Klient>(restauracja.PobierzListeKlientow());
                    break;
                case "Dania":
                    lstDane.ItemsSource = new ObservableCollection<Danie>(restauracja.Dania);
                    break;
                case "Konta":
                    lstDane.ItemsSource = new ObservableCollection<Konto>(restauracja.Konta);
                    break;
            }
        }

        private void zmianaTabeli(object sender, RoutedEventArgs e)
        {
            Button? pressedButton = sender as Button;
            if (pressedButton != null)
            {

                switch (pressedButton.Name)
                {
                    case "btnDania":
                        obecnaTabela = "dania";
                        inicjalizacjaTabeli();
                        break;
                    case "btnPracownicy":
                        obecnaTabela = "pracownicy";
                        inicjalizacjaTabeli();
                        break;
                    case "btnKlienci":
                        obecnaTabela = "klienci";
                        inicjalizacjaTabeli();
                        break;
                    case "btnKonta":
                        obecnaTabela = "konta";
                        inicjalizacjaTabeli();
                        break;
                    default:
                        break;

                }
            }
        }

        private void addPressed(object sender, RoutedEventArgs e)
        {

        }

        private void editPressed(object sender, RoutedEventArgs e)
        {

        }

        private void deletePressed(object sender, RoutedEventArgs e)
        {

        }
    }
}
