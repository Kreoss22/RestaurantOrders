using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Pracownik : Osoba
    {
        string pozycja;
        bool czyKucharz;
        string pesel;

        public string Pesel
        {
            get => pesel; init
            {
                if (!Regex.IsMatch(value, @"\d{11}"))
                {
                    throw new ArgumentException("Pesel musi mieć 11 znaków");
                }
                pesel = value;
            }
        }

    }
}
