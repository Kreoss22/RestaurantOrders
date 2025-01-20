using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    abstract class Osoba
    {
        protected string imie;
        protected string nazwisko;
        protected string email;
        protected string nrTel;

        public string Email
        {
            get => email; init
            {
                if (!Regex.IsMatch(value, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}"))
                {
                    throw new ArgumentException("Zły format adresu email.");
                }
                email = value;
            }
        }

        public string NrTel
        {
            get => nrTel; init
            {
                if (!Regex.IsMatch(value, @"\d{9}"))
                {
                    throw new ArgumentException("Błędny numer telefonu.");
                }
                nrTel = value;
            }
        }
    }
}
