using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Restauracja r = new Restauracja("@pizz.com", "Pizzastic");
            r.DodajKontoKlienta("Jan", "Kubacki", "jkub@gmail.com", "543512321", "haslo1");
            r.DodajKontoKlienta("Ewa", "Nowak", "nov@gmail.com", "232112312", "password");
            r.DodajKontoPracownika("Kamil", "Robak","kamir@gmail.com", "621341232","Kelner",false, "62341231232","Pracownikhaslo1",EnumUprawienia.pracownik);
            Console.WriteLine(r);
            bool result = r.ZapiszXML("restauracja.xml");
            Console.WriteLine(result ? "Serialization succeeded!" : "Serialization failed.");
        }
    }
}
