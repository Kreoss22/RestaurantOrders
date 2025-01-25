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
    /// Logika interakcji dla klasy DodajDanie.xaml
    /// </summary>
    public partial class DodajDanie : Window
    {
        Danie danie;
        public DodajDanie(Danie danie)
        {
            InitializeComponent();
            this.danie = danie;
            txtCena.Text = danie.Cena.ToString();
            txtNazwa.Text = danie.Nazwa;
            cmbKategoria.SelectedIndex = -1;
        }

        public void DodajBtnClicked(object sender, RoutedEventArgs e)
        {
            if (txtCena.Text != "" || txtNazwa.Text != "" || cmbKategoria.SelectedIndex != -1)
            {
                danie.Nazwa = txtNazwa.Text;
                if (double.TryParse(txtCena.Text, out double value))
                {
                    danie.Cena = value;
                    if (cmbKategoria.SelectedItem is ComboBoxItem selectedItem)
                    {
                        danie.Kategoria = selectedItem.Content.ToString();
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Błędna kategoria");
                    }
                }
                else
                {
                    MessageBox.Show("Cena nie jest liczbą");
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


        //sprawdza czy w textboxie jest liczba
        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _);
        }
    }
}
