using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Restauracja
    {
        Dictionary<string, Zamowienie> zamowienia; // klucz - id zamowienia
        List<Danie> dania;
        private List<Konto> konta;
        private List<Pracownik> pracownicy;
        string domena;
        string nazwa;


        public Restauracja(string domena, string nazwa)
        {
            this.domena = domena;
            this.nazwa = nazwa;
            this.pracownicy = new List<Pracownik>();
            this.konta = new List<Konto>();
            this.zamowienia = new Dictionary<string, Zamowienie>();
            this.dania = new List<Danie>();
        }
    }
}
