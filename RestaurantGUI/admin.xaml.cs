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
using System.CodeDom;

namespace RestaurantGUI
{
    /// <summary>
    /// Logika interakcji dla klasy Admin.xaml
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
            InicjalizacjaTabeli();
        }

        private void InicjalizacjaTabeli()
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

        private void ZapisPressed(object sender, RoutedEventArgs e)
        {
            restauracja.ZapiszXML("restauracja.xml");
        }

        private void WylogujPressed(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }

        private void ZmianaTabeli(object sender, RoutedEventArgs e)
        {
            Button? pressedButton = sender as Button;
            if (pressedButton != null)
            {
                switch (pressedButton.Name)
                {
                    case "btnDania":
                        obecnaTabela = "dania";
                        InicjalizacjaTabeli();
                        break;
                    case "btnPracownicy":
                        obecnaTabela = "pracownicy";
                        InicjalizacjaTabeli();
                        break;
                    case "btnKlienci":
                        obecnaTabela = "klienci";
                        InicjalizacjaTabeli();
                        break;
                    case "btnKonta":
                        obecnaTabela = "konta";
                        InicjalizacjaTabeli();
                        break;
                    default:
                        tableNameLabel.Content = "Tabela ";
                        break;

                }
            }
        }

        private void AddPressed(object sender, RoutedEventArgs e)
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
                        this.restauracja.DodajKonto(kontoKlienta);
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
                        this.restauracja.DodajKonto(kontoPracownika);
                        restauracja.SortujKonta();
                        lstDane.ItemsSource = new ObservableCollection<Konto>(this.restauracja.Konta);
                    }
                    break;
            }
        }

        private void EditPressed(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePressed(object sender, RoutedEventArgs e)
        {
            if(lstDane.SelectedIndex > -1)
            {
                switch (this.obecnaTabela)
                {
                    case "pracownicy":
                        Pracownik? selectedEmployee = lstDane.SelectedItem as Pracownik;
                        if (selectedEmployee is not null)
                        {
                            restauracja.UsunPracownika(selectedEmployee.Pesel);
                        }
                        lstDane.ItemsSource = new ObservableCollection<Pracownik>(restauracja.Pracownicy);
                        break;
                    case "klienci":
                        Klient? selectedClient = lstDane.SelectedItem as Klient;
                        if (selectedClient is not null)
                        {
                            restauracja.UsunKonto(selectedClient.Email);
                        }
                        lstDane.ItemsSource = new ObservableCollection<Klient>(this.restauracja.PobierzListeKlientow());
                        break;
                    case "dania":
                        Danie? selectedMeal = lstDane.SelectedItem as Danie;
                        if (selectedMeal is not null)
                        {
                            restauracja.UsunDanie(selectedMeal.Nazwa);
                        }
                        lstDane.ItemsSource = new ObservableCollection<Danie>(restauracja.Dania);
                        break;
                    case "konta":
                        Konto? selectedAccount = lstDane.SelectedItem as Konto;
                        if (selectedAccount is not null)
                        {
                            restauracja.UsunKonto(selectedAccount.Login);
                            restauracja.SortujKonta();
                        }
                        lstDane.ItemsSource = new ObservableCollection<Konto>(restauracja.Konta);
                        break;
                }
            }
        }

        private void ZamowieniaPressed(object sender, RoutedEventArgs e)
        {
            OknoKelner okno = new OknoKelner(restauracja, EnumUprawienia.admin);
            okno.Show();
            this.Close();
        }
    }
}
