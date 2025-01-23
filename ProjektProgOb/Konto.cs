using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum EnumUprawienia
    {
        klient,
        admin,
        pracownik
    }
    public class Konto
    {
        string login;
        string haslo;
        Osoba wlasciciel;
        EnumUprawienia uprawienia;

        public string Login {  get { return login; } set { login = value; } }
        public string Haslo { get => haslo; set => haslo = value; }
        public EnumUprawienia Uprawienia { get => uprawienia; set => uprawienia = value; }
        public Osoba Wlasciciel { get => wlasciciel; set => wlasciciel = value; }

        public Konto(Klient klient, string haslo)
        {
            this.Haslo = haslo;
            this.login = klient.Email;
            this.Uprawienia = EnumUprawienia.klient;
            this.Wlasciciel = klient;
        }

        public Konto(Pracownik pracownik, string haslo, EnumUprawienia uprawienia)
        {
            this.Haslo = haslo;
            this.login = pracownik.Email;
            this.Uprawienia = uprawienia;
            this.Wlasciciel = pracownik;
        }
    }
}
