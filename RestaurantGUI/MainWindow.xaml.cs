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
            restauracja = new Restauracja("dobre_jedzenie.pl", "Dobre Jedzenie");
            RestaurantName.Content = $"Restauracja \"{restauracja.Nazwa}\"";
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text;
            string haslo = passwordTb.Password;
            foreach(Konto konto in restauracja.Konta)
            {
                if(konto.Login == login && konto.Haslo == haslo)
                {
                    switch (konto.Uprawienia)
                    {
                        case EnumUprawienia.admin:
                            Admin admin = new Admin();
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
            }
            Admin admin1 = new Admin();
            admin1.Show();
            this.Close();
        }

        private void RegisterBtnCLick(object sender, RoutedEventArgs e)
        {

        }
    }
}