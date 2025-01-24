﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum EnumUprawienia
    {
        admin = 0,
        pracownik = 1,
        klient = 2
    }
    [DataContract]
    public class Konto : IComparable<Konto>, IEquatable<Konto>
    {
        [DataMember]
        string login;
        [DataMember]
        string haslo;
        [DataMember]
        Osoba wlasciciel;
        [DataMember]
        EnumUprawienia uprawienia;

        public string Login {  get { return login; } set { login = value; } }
        public string Haslo { get => haslo; set => haslo = value; }
        public EnumUprawienia Uprawienia { get => uprawienia; set => uprawienia = value; }
        public Osoba Wlasciciel { get => wlasciciel; set => wlasciciel = value; }

        public Konto(EnumUprawienia uprawienia, Osoba wlasciciel)
        {
            this.Haslo = string.Empty;
            this.Login = string.Empty;
            this.Uprawienia = uprawienia;
            this.Wlasciciel = wlasciciel;
        }

        public Konto(Klient klient, string haslo)
        {
            this.Haslo = haslo;
            this.login = klient.Email;
            this.Uprawienia = EnumUprawienia.klient;
            this.Wlasciciel = klient;
        }

        public Konto(Pracownik pracownik, string haslo, EnumUprawienia uprawienia, string login)
        {
            Haslo = haslo;
            Login = login;
            Uprawienia = uprawienia;
            Wlasciciel = pracownik;
        }

        public override string ToString()
        {
            string uprawnieniaString = "";
            switch (uprawienia)
            {
                case EnumUprawienia.klient:
                    uprawnieniaString = "klient";
                    break;
                case EnumUprawienia.admin:
                    uprawnieniaString = "admin";
                    break;
                case EnumUprawienia.pracownik:
                    uprawnieniaString = "pracownik";
                    break;

            }
            return $"{uprawnieniaString} {Login} {wlasciciel.Imie} {wlasciciel.Nazwisko}";
        }

        public int CompareTo(Konto other)
        {
            if (other == null)
            {
                return 1;
            }
            int result = uprawienia.CompareTo(other.uprawienia);
            if (result != 0) return result;

            result = string.Compare(wlasciciel.Nazwisko, other.wlasciciel.Nazwisko, StringComparison.Ordinal);
            if (result != 0) return result;

            return string.Compare(wlasciciel.Imie, other.wlasciciel.Imie, StringComparison.Ordinal);

            throw new NotImplementedException();
        }

        public bool Equals(Konto other)
        {
            if (other is null) return false;
            return login.Equals(other.login);
        }
    }
}
