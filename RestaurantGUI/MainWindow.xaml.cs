using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Restaurant;
using static System.Net.Mime.MediaTypeNames;

namespace RestaurantGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Restauracja restauracja;
        public MainWindow()
        {
            InitializeComponent();
            restauracja = Restauracja.OdczytajXml("restauracja.xml");
            restauracja.SortujKonta();
            RestaurantName.Content = $"Restauracja \"{restauracja.Nazwa}\"";
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text;
            string haslo = passwordTb.Password;
            Konto? konto = restauracja.Konta.FirstOrDefault(k => k.Login == login && k.Haslo == haslo);
            if (konto != null)
            {
                switch (konto.Uprawienia)
                {
                    case EnumUprawienia.admin:
                        Admin admin = new Admin(this.restauracja, "pracownicy");
                        admin.Show();
                        this.Close();
                        break;
                    case EnumUprawienia.pracownik:
                        OknoKelner okno = new OknoKelner(restauracja, EnumUprawienia.pracownik);
                        okno.Show();
                        this.Close();
                        break;
                    case EnumUprawienia.klient:
                        DodajZamowienie dodawanieZamowienia = new DodajZamowienie(restauracja, konto.Uprawienia, konto.Login);
                        dodawanieZamowienia.Show();
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("B³êdny login lub has³o");
            }
        }

        private void RegisterBtnCLick(object sender, RoutedEventArgs e)
        {
            Konto kontoKlienta = new Konto(EnumUprawienia.klient, new Klient());
            DodajKlienta oknoKlienta = new DodajKlienta(kontoKlienta);
            bool? result = oknoKlienta.ShowDialog();
            if (result == true)
            {
                this.restauracja.DodajKonto(kontoKlienta);
            }
        }
    }
}