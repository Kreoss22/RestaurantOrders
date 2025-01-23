using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    [DataContract]
    public class Zamowienie
    {
        [DataMember]
        private string idZamowienia;
        [DataMember]
        private List<(Danie, int)> zamowioneDania; //int - ilosc
        [DataMember]
        private DateTime dataZamowienia;
        [DataMember]
        private int oplaty;
        [DataMember]
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
