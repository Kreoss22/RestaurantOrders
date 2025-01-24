using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    [DataContract]
    [KnownType(typeof(Klient))]
    public class Klient : Osoba
    {
        [DataMember]
        List<Zamowienie> listaZamowien;

        public Klient() : base() 
        {
            listaZamowien = new List<Zamowienie>();
        }

        public Klient(string imie, string nazwisko, string email, string nrTel) : base(imie,nazwisko, email, nrTel)
        {
            listaZamowien = new List<Zamowienie>();
        }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Email} {NrTel} l.zam.: {listaZamowien.Count()}";
        }
    }
}
