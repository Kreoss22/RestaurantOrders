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
    /// Logika interakcji dla klasy DodajKonto.xaml
    /// </summary>
    public partial class DodajKonto : Window
    {
        private Konto konto;
        List<Pracownik> pracownicy;
        public DodajKonto(Konto konto, List<Pracownik> pracownicy)
        {
            InitializeComponent();
            this.konto = konto;
            this.pracownicy = pracownicy;
            txtHaslo.Text = konto.Haslo;
            txtLogin.Text = konto.Login;
            txtWlasciciel.Text = konto.Wlasciciel.Email;
        }

        public DodajKonto(Konto konto)
        {
            InitializeComponent();
            this.konto = konto;
            this.pracownicy = new List<Pracownik>();
            txtHaslo.Text = konto.Haslo;
            txtLogin.Text = konto.Login;
            txtWlasciciel.Text = konto.Wlasciciel.Email;
            txtWlasciciel.IsReadOnly = true;
        }

        public void DodajBtnClicked(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != "" || txtHaslo.Text != "" || txtWlasciciel.Text != "")
            {
                if(pracownicy.Count == 0)
                {
                    konto.Haslo = txtHaslo.Text;
                    konto.Login = txtLogin.Text;
                    DialogResult = true;
                }
                var pracownik = pracownicy.FirstOrDefault(p => p.Email == txtWlasciciel.Text);
                if (pracownik == null)
                {
                    MessageBox.Show("Prak pracownika z danym adresem email");
                }
                else
                {
                    konto.Wlasciciel = pracownik;
                    konto.Haslo = txtHaslo.Text;
                    konto.Login = txtLogin.Text;
                    DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola");
            }
        }

        public void AnulujBtnClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
