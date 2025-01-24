using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum EnumStanZamowienia
    {
        Przyjeto,
        Realizacja,
        Zrealizowano
    }
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
        private EnumStanZamowienia stanZamowienia;
        [DataMember]
        private decimal oplaty;
        [DataMember]
        static int indeks;

        public string IdZamowienia { get => idZamowienia; set => idZamowienia = value; }
        internal EnumStanZamowienia StanZamowienia { get => stanZamowienia; set => stanZamowienia = value; }

        static Zamowienie()
        {
            indeks = 0;
        }

        public Zamowienie(List<(Danie, int)> zamowioneDania, DateTime dataZamowienia, int oplaty)
        {
            this.dataZamowienia = dataZamowienia;
            this.zamowioneDania = zamowioneDania;
            IdZamowienia = $"{dataZamowienia.Year}{dataZamowienia.Month}{dataZamowienia.Day}{indeks.ToString("D7")}";
            this.oplaty = oplaty;
            indeks++;
        }

        public decimal CenaZamowienia()
        {
            decimal suma = zamowioneDania.Sum(d => d.Item1.Cena * d.Item2);
            return suma + oplaty;
        }

        public override string ToString()
        {
            return $"{IdZamowienia} {StanZamowienia.ToString()} cena:{this.CenaZamowienia()}zł, l.dań:{zamowioneDania.Sum(d => d.Item2)}";
        }
    }
}
