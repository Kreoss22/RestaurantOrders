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
using static System.Net.Mime.MediaTypeNames;

namespace RestaurantGUI
{
    /// <summary>
    /// Logika interakcji dla klasy DodajKlienta.xaml
    /// </summary>
    public partial class DodajKlienta : Window
    {
        Konto konto;
        public DodajKlienta(Konto konto)
        {
            InitializeComponent();
            this.konto = konto;
        }

        public void DodajBtnClicked(object sender, RoutedEventArgs e)
        {
            if (txtImie.Text != "" || txtNazwisko.Text != "" || txtNrTel.Text != "" || txtEmail.Text != "" || txtHaslo.Text != "")
            {
                Klient klient = new Klient(txtImie.Text, txtNazwisko.Text, txtEmail.Text, txtNrTel.Text);
                konto.Haslo = txtHaslo.Text;
                konto.Wlasciciel = klient;
                konto.Login = klient.Email;
                DialogResult = true;
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
