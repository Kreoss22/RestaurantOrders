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
            List<string> kategorie = new List<string>
            {
                "Przystawki",
                "Zupy",
                "Pizza",
                "Makarony",
                "Dania g³ówne",
                "Napoje zimne",
                "Napoje gor¹ce",
                "Piwo",
                "Wino"
            };
            restauracja = new Restauracja("pizz.pl", "Pizzastic", kategorie);
            Pracownik admin = new Pracownik("admin", false, "11111111111", "Pawe³", "Modzelewski", "pmodzelewski@student.agh.edu.pl", "999999999");
            Konto adminKonto = new Konto(admin, "admin123", EnumUprawienia.admin, $"admin@{restauracja.Domena}"); //admin@pizz.pl
            restauracja.DodajPracownika(admin);
            restauracja.DodajKonto(adminKonto);
            Pracownik kelner = new Pracownik("kelner", false, "01010101012", "Ala", "Kot", "alaKot@gmail.com", "101010101");
            Konto kelnerKonto = new Konto(kelner, "kelner123", EnumUprawienia.pracownik, $"alaKot@{restauracja.Domena}");
            restauracja.DodajPracownika(kelner);
            restauracja.DodajKonto(kelnerKonto);
            Konto klientKonto = new Konto(new Klient("Jan", "Nowak", "jnowak@interia.pl", "121212121"), "klient123");
            restauracja.DodajKonto(klientKonto);
            restauracja.ZapiszXML("restauracja.xml");
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

        }
    }
}