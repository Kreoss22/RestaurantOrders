﻿using System;
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
    /// <summary>
    /// Klasa bazowa reprezentująca osobę.
    /// </summary>
    [DataContract]
    public abstract class Osoba
    {
        [DataMember]
        private string imie;
        [DataMember]
        private string nazwisko;
        [DataMember]
        private string? email;
        [DataMember]
        private string? nrTel;

        /// <summary>
        /// Pobiera lub ustawia adres e-mail osoby.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucane, gdy adres e-mail nie jest w poprawnym formacie.</exception>
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

        /// <summary>
        /// Pobiera lub ustawia numer telefonu osoby.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucane, gdy numer telefonu nie jest w poprawnym formacie.</exception>
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

        
        public Osoba()
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
        }

        
        public Osoba(string imie, string nazwisko, string email, string nrTel)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Email = email;
            NrTel = nrTel;
        }
    }
}
