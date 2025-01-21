using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Klient : Osoba
    {
        List<Zamowienie> listaZamowien;

        public Klient(string imie, string nazwisko, string email, string nrTel) : base(imie,nazwisko, email, nrTel)
        {
            listaZamowien = new List<Zamowienie>();
        }
    }
}
