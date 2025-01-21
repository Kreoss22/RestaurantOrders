using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    enum EnumUprawienia
    {
        klient,
        admin,
        pracownik
    }
    internal class Konto
    {
        string login;
        string haslo;
        Osoba wlasciciel;
        EnumUprawienia uprawienia;

        public string Login {  get { return login; } set { login = value; } }

        public Konto(Klient klient, string haslo)
        {
            this.haslo = haslo;
            this.login = klient.Email;
            this.uprawienia = EnumUprawienia.klient;
            this.wlasciciel = klient;
        }

        public Konto(Pracownik pracownik, string haslo, EnumUprawienia uprawienia)
        {
            this.haslo = haslo;
            this.login = pracownik.Email;
            this.uprawienia = uprawienia;
            this.wlasciciel = pracownik;
        }
    }
}
