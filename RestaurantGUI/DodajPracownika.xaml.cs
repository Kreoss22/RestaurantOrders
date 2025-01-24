using Restaurant;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logika interakcji dla klasy DodajPracownika.xaml
    /// </summary>
    public partial class DodajPracownika : Window
    {
        private Pracownik pracownik;

        public DodajPracownika(Pracownik pracownik)
        {
            InitializeComponent();
            this.pracownik = pracownik;
            txtImie.Text = pracownik.Imie;
            txtNazwisko.Text = pracownik.Nazwisko;
            txtEmail.Text = pracownik.Email;
            txtNrTel.Text = pracownik.NrTel;
            txtPesel.Text = pracownik.Pesel;
            txtPozycja.Text = pracownik.Pozycja;
            chkCzyKucharz.IsChecked = pracownik.CzyKucharz;
        }

        public void DodajBtnClicked(object sender, RoutedEventArgs e)
        {
            if (txtPesel.Text != "" || txtImie.Text != "" || txtNazwisko.Text != "" || txtNrTel.Text != "" || txtEmail.Text != "" || txtPozycja.Text != "")
            {
                pracownik.Pesel = txtPesel.Text;
                pracownik.Imie = txtImie.Text;
                pracownik.Nazwisko = txtNazwisko.Text;
                pracownik.Email = txtEmail.Text;
                pracownik.Pesel = txtPesel.Text;
                pracownik.Pozycja = txtPozycja.Text;
                pracownik.CzyKucharz = chkCzyKucharz.IsChecked;
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
