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
                "Dania g��wne",
                "Napoje zimne",
                "Napoje gor�ce",
                "Piwo",
                "Wino"
            };
            restauracja = new Restauracja("@pizz.pl", "Pizzastic", kategorie);
            Pracownik admin = new Pracownik("admin", false, "11111111111", "Pawe�", "Modzelewski", "pmodzelewski@student.agh.edu.pl", "999999999");
            Konto adminKonto = new Konto(admin, "admin123", EnumUprawienia.admin, $"admin@{restauracja.Domena}"); //admin@pizz.pl
            restauracja.DodajPracownika(admin);
            restauracja.DodajKontoPracownika(adminKonto);
            restauracja.ZapiszXML("restauracja.xml");
            RestaurantName.Content = $"Restauracja \"{restauracja.Nazwa}\"";
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text;
            string haslo = passwordTb.Password;
            MessageBox.Show($"{login} {haslo}");
            Konto konto = restauracja.Konta.Single();
            switch (konto.Uprawienia)
            {
                case EnumUprawienia.admin:
                    Admin admin = new Admin(this.restauracja, "pracownicy");
                    admin.Show();
                    this.Close();
                    break;
                case EnumUprawienia.pracownik:
                    break;
                case EnumUprawienia.klient:
                    break;
                default:
                    break;
            }
            
                
        }

        private void RegisterBtnCLick(object sender, RoutedEventArgs e)
        {

        }
    }
}