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
using System.Windows.Controls.Primitives;

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
            inicjalizacjaTabeli();
        }

        private void inicjalizacjaTabeli()
        {
            lstDane.ItemsSource = null;
            switch (this.obecnaTabela)
            {
                case "pracownicy":
                    tableNameLabel.Content = "Tabela Pracownicy";
                    lstDane.ItemsSource = new ObservableCollection<Pracownik>(restauracja.Pracownicy);
                    break;
                case "klienci":
                    tableNameLabel.Content = "Tabela Klienci";
                    lstDane.ItemsSource = new ObservableCollection<Klient>(restauracja.PobierzListeKlientow());
                    break;
                case "dania":
                    tableNameLabel.Content = "Tabela Dania";
                    lstDane.ItemsSource = new ObservableCollection<Danie>(restauracja.Dania);
                    break;
                case "konta":
                    tableNameLabel.Content = "Tabela Konta";
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
                        tableNameLabel.Content = "Tabela ";
                        break;

                }
            }
        }

        private void addPressed(object sender, RoutedEventArgs e)
        {
            bool? result;
            switch (this.obecnaTabela)
            {
                case "pracownicy":
                    Pracownik pracownik = new Pracownik();
                    DodajPracownika oknoPracownika = new DodajPracownika(pracownik);
                    result = oknoPracownika.ShowDialog(); if
                    (result == true)
                    {
                        this.restauracja.DodajPracownika(pracownik);
                        lstDane.ItemsSource = new ObservableCollection<Pracownik>(this.restauracja.Pracownicy);
                    }
                    break;
                case "klienci":
                    Konto kontoKlienta = new Konto(EnumUprawienia.klient, new Klient());
                    DodajKlienta oknoKlienta = new DodajKlienta(kontoKlienta);
                    result = oknoKlienta.ShowDialog();
                    if(result == true)
                    {
                        this.restauracja.DodajKontoKlienta(kontoKlienta);
                        lstDane.ItemsSource = new ObservableCollection<Klient>(this.restauracja.PobierzListeKlientow());
                    }
                    break;
                case "dania":
                    Danie danie = new Danie();
                    DodajDanie oknoDanie = new DodajDanie(danie);
                    result = oknoDanie.ShowDialog();
                    if (result == true)
                    {
                        this.restauracja.DodajDanie(danie);
                        lstDane.ItemsSource = new ObservableCollection<Klient>(this.restauracja.PobierzListeKlientow());
                    }
                    break;
                case "konta":
                    Konto kontoPracownika = new Konto(EnumUprawienia.pracownik, new Pracownik());
                    DodajKonto oknoKonta = new DodajKonto(kontoPracownika, restauracja.Pracownicy);
                    result = oknoKonta.ShowDialog();
                    if(result == true)
                    {
                        this.restauracja.DodajKontoPracownika(kontoPracownika);
                        lstDane.ItemsSource = new ObservableCollection<Konto>(this.restauracja.Konta);
                    }
                    break;
            }
        }

        private void editPressed(object sender, RoutedEventArgs e)
        {

        }

        private void deletePressed(object sender, RoutedEventArgs e)
        {

        }
    }
}
