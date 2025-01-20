using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Zamowienie
    {
        private string idZamowienia;
        private List<(Danie, int)> zamowioneDania;
        private DateTime dataZamowienia;
        static int indeks;
        
        static Zamowienie()
        {
            indeks = 0;
        }

        public Zamowienie(List<(Danie, int)> zamowioneDania, DateTime dataZamowienia)
        {
            this.dataZamowienia = dataZamowienia;
            this.zamowioneDania = zamowioneDania;

        }
    }
}
