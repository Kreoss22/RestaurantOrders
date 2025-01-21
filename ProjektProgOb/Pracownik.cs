using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Pracownik : Osoba
    {
        string pozycja;
        bool czyKucharz;
        string pesel;
        bool czyAktywny;

        public string Pesel
        {
            get => pesel; set
            {
                if (!Regex.IsMatch(value, @"\d{11}"))
                {
                    throw new ArgumentException("Pesel musi mieć 11 znaków");
                }
                pesel = value;
            }
        }

        internal string Pozycja { get => pozycja; set => pozycja = value; }
        public bool CzyKucharz { get => czyKucharz; set => czyKucharz = value; }

        public Pracownik(string pozycja, bool czyKucharz, string pesel, string imie, string nazwisko, string email, string nrTel) : base(imie, nazwisko, email, nrTel)
        {
            Pozycja = pozycja;
            CzyKucharz = czyKucharz;
            Pesel = pesel;
            czyAktywny = true;
        }
    }
}
