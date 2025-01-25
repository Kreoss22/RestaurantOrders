using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant
{
    /// <summary>
    /// Stan zamówienia.
    /// </summary>
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
        private List<(Danie, int)> zamowioneDania; //int - ilość

        [DataMember]
        private DateTime dataZamowienia;

        [DataMember]
        private EnumStanZamowienia stanZamowienia;

        [DataMember]
        private double oplaty;

        [DataMember]
        static int indeks;

        
        public string IdZamowienia { get => idZamowienia; set => idZamowienia = value; }

      
        public EnumStanZamowienia StanZamowienia { get => stanZamowienia; set => stanZamowienia = value; }

        static Zamowienie()
        {
            indeks = 0;
        }

        /// <summary>
        /// Tworzy nowe zamówienie.
        /// </summary>
        /// <param name="zamowioneDania">Lista zamówionych dań.</param>
        /// <param name="dataZamowienia">Data zamówienia.</param>
        /// <param name="oplaty">Opłaty dodatkowe.</param>
        public Zamowienie(List<(Danie, int)> zamowioneDania, DateTime dataZamowienia, double oplaty)
        {
            this.dataZamowienia = dataZamowienia;
            this.zamowioneDania = zamowioneDania;
            IdZamowienia = $"{dataZamowienia.Year}{dataZamowienia.Month}{dataZamowienia.Day}{indeks.ToString("D7")}";
            this.oplaty = oplaty;
            indeks++;
            StanZamowienia = EnumStanZamowienia.Przyjeto;
        }

        /// <summary>
        /// Oblicza cenę zamówienia.
        /// </summary>
        /// <returns>Cena całkowita.</returns>
        public double CenaZamowienia()
        {
            double suma = zamowioneDania.Sum(d => d.Item1.Cena * d.Item2);
            return suma + oplaty;
        }

        /// <summary>
        /// Opis zamówienia w postaci tekstowej.
        /// </summary>
        /// <returns>Opis zamówienia.</returns>
        public override string ToString()
        {
            return $"{IdZamowienia} {StanZamowienia.ToString()} cena:{this.CenaZamowienia()}zł, l.dań:{zamowioneDania.Sum(d => d.Item2)}";
        }
    }
}
