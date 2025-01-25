using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant
{
    /// <summary>
    /// Klasa reprezentująca pracownika restauracji, dziedzicząca po klasie Osoba.
    /// </summary>
    [DataContract]
    [KnownType(typeof(Pracownik))]
    public class Pracownik : Osoba
    {
        [DataMember]
        string pozycja;
        [DataMember]
        bool? czyKucharz;
        [DataMember]
        string? pesel;

        /// <summary>
        /// Pobiera lub ustawia numer PESEL pracownika.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucane, gdy PESEL nie ma 11 znaków.</exception>
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

       
        public string Pozycja { get => pozycja; set => pozycja = value; }
        public bool? CzyKucharz { get => czyKucharz; set => czyKucharz = value; }

        
        public Pracownik() : base()
        {
            Pozycja = string.Empty;
            CzyKucharz = false;
        }

        
        public Pracownik(string pozycja, bool czyKucharz, string pesel, string imie, string nazwisko, string email, string nrTel) : base(imie, nazwisko, email, nrTel)
        {
            Pozycja = pozycja;
            CzyKucharz = czyKucharz;
            Pesel = pesel;
        }

        public override string ToString()
        {
            return $"{this.Imie} {this.Nazwisko} - {this.Pozycja}, {NrTel} {Email}";
        }
    }
}

