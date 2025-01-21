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
        string hasło;
        Osoba wlasciciel;
        EnumUprawienia uprawienia;

        public string Login {  get { return login; } set { login = value; } }
    }
}
