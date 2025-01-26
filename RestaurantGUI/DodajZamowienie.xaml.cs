using Restaurant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
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
    /// Logika interakcji dla klasy panel.xaml
    /// </summary>
    public partial class DodajZamowienie : Window
    {
        Restauracja restauracja;
        string obecnaKategoria;
        List<(Danie, int)> zamowioneDania;
        EnumUprawienia uprawnienia;
        string loginKlienta;
        public DodajZamowienie(Restauracja restaurant, EnumUprawienia uprawienia, string login)
        {
            InitializeComponent();
            restauracja = restaurant;
            this.uprawnienia = uprawienia;
            this.loginKlienta = login;
            TworzenieNowegoZamowienia();
            foreach (string kategoria in restauracja.KategorieDan)
            {
                Button button = new Button
                {
                    Content = kategoria,
                    Width = 160,
                    Height = 30,
                    Margin = new Thickness(5),
                    FontSize = 16,
                    Background = Brushes.CornflowerBlue,
                    Foreground = Brushes.White
                };

                // Dodanie zdarzenia kliknięcia
                button.Click += CategoryButtonClicked;

                // Dodanie przycisku do kontenera
                categoryStack.Children.Add(button);
            }

        }

        private void TworzenieNowegoZamowienia()
        {
            obecnaKategoria = "Przystawki";
            zamowioneDania = new List<(Danie, int)>();
            txtIlosc.Text = "0";
            txtNapiwek.Text = "0";
            List<Danie> daniaZKategorii = restauracja.Dania.Where(d => d.Kategoria == obecnaKategoria).ToList();
            lstDania.ItemsSource = new ObservableCollection<Danie>(daniaZKategorii);
        }

        private void CategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            Button? pressedButton = sender as Button;
            if (pressedButton != null)
            {
                string obecnaKategoria = (string)pressedButton.Content;
                List<Danie> daniaZKategorii = restauracja.Dania.Where(d => d.Kategoria == obecnaKategoria).ToList();
                lstDania.ItemsSource = new ObservableCollection<Danie>(daniaZKategorii);
                categoryLabel.Content = $"{obecnaKategoria}:";
            }
        }

        private void EscapeButtonClicked(object sender, RoutedEventArgs e)
        {
            restauracja.ZapiszXML("restauracja.xml");
            if (uprawnienia == EnumUprawienia.klient)
            {
                MainWindow okno = new MainWindow();
                okno.Show();
                this.Close();
            }
            else
            {
                OknoKelner okno = new OknoKelner(restauracja, uprawnienia);
                okno.Show();
                this.Close();
            }
        }
        
        private void ZapisClicked(object sender, RoutedEventArgs e)
        {
            restauracja.ZapiszXML("restauracja.xml");
        }

        private void NapiwekTextBoxChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _);
        }

        private void IloscTextBoxChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void DodajDanieButtonClicked(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtIlosc.Text, out int x))
            {
                int ilosc = x;
                if (ilosc != 0)
                {
                    Danie? dodaneDanie = lstDania.SelectedItem as Danie;
                    if (dodaneDanie != null)
                    {
                        (Danie, int) danieWLiscie = zamowioneDania.FirstOrDefault(d => d.Item1.Equals(dodaneDanie));
                        if (danieWLiscie.Item1 != null)
                        {
                            danieWLiscie.Item2 = ilosc;
                        }
                        else
                        {
                            zamowioneDania.Add((dodaneDanie, ilosc));
                        }
                        lstDania.SelectedIndex = -1;
                        txtIlosc.Text = "0";
                    }
                }
            }
        }

        private void DodajZamowienieButtonClicked(object sender, RoutedEventArgs e)
        {
            if (zamowioneDania.Count > 0)
            {
                if (double.TryParse(txtNapiwek.Text, out double x))
                {
                    double napiwek = x;
                    Zamowienie noweZamowienie = new Zamowienie(zamowioneDania, DateTime.Now, x);
                    restauracja.Zamowienia.Add(noweZamowienie.IdZamowienia, noweZamowienie);
                    if(this.uprawnienia == EnumUprawienia.klient)
                    {
                        Konto? kontoKlienta = restauracja.Konta.FirstOrDefault(k => k.Login == loginKlienta);
                        if (kontoKlienta != null)
                        {
                            Klient? klient = kontoKlienta.Wlasciciel as Klient;
                            if(klient != null)
                            {
                                klient.ListaZamowien.Add(noweZamowienie);
                            }
                        }
                    }
                    TworzenieNowegoZamowienia();
                }
            }
            else
            {
                MessageBox.Show("Dodaj dania do zamówienia!");
            }
        }


        private void lstDania_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDania.SelectedItem != null)
            {
                if (lstDania.SelectedIndex != -1)
                {
                    Danie? wybraneDanie = lstDania.SelectedItem as Danie;
                    (Danie,int) danieWLiscie = zamowioneDania.FirstOrDefault(d => d.Item1.Equals(wybraneDanie));
                    if(danieWLiscie.Item1 != null)
                    {
                        txtIlosc.Text = danieWLiscie.Item2.ToString();
                    }
                }
            }
        }
    }
}
