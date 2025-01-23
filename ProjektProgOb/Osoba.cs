using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Restaurant
{
    [DataContract]
    public abstract class Osoba
    {
        [DataMember]
        private string imie;
        [DataMember]
        private string nazwisko;
        [DataMember]
        private string email;
        [DataMember]
        private string nrTel;

        public string Email
        {
            get => email; set
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
            get => nrTel; set
            {
                if (!Regex.IsMatch(value, @"\d{9}"))
                {
                    throw new ArgumentException("Błędny numer telefonu.");
                }
                nrTel = value;
            }
        }
        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }

        public Osoba(string imie, string nazwisko, string email, string nrTel)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Email = email;
            NrTel = nrTel;
        }
    }
}
