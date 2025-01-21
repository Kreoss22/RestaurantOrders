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
        private List<(Danie, int)> zamowioneDania; //int - ilosc
        private DateTime dataZamowienia;
        private int oplaty;
        static int indeks;

        static Zamowienie()
        {
            indeks = 0;
        }

        public Zamowienie(List<(Danie, int)> zamowioneDania, DateTime dataZamowienia, int oplaty)
        {
            this.dataZamowienia = dataZamowienia;
            this.zamowioneDania = zamowioneDania;
            idZamowienia = $"{dataZamowienia.Year}{dataZamowienia.Month}{dataZamowienia.Day}{indeks.ToString("D7")}";
            this.oplaty = oplaty;
        }
    }
}
