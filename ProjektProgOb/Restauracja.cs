﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Restauracja
    {
        Dictionary<string, Zamowienie> zamowienia; // klucz - id zamowienia
        List<Danie> dania;
        private List<Konto> konta;
        private List<Pracownik> pracownicy;
        private List<string> kategorieDan;
        private string domena;
        private string nazwa;

        public List<string> KategorieDan { get => kategorieDan; set => kategorieDan = value; }
        public List<Konto> Konta { get => konta; set => konta = value; }
        public List<Pracownik> Pracownicy { get => pracownicy; set => pracownicy = value; }
        public Dictionary<string, Zamowienie> Zamowienia { get => zamowienia; set => zamowienia = value; }
        public List<Danie> Dania { get => dania; set => dania = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }
        public string Domena { get => domena; set => domena = value; }

        public Restauracja(string domena, string nazwa)
        {
            this.Domena = domena;
            this.Nazwa = nazwa;
            this.Pracownicy = new List<Pracownik>();
            this.Konta = new List<Konto>();
            this.Dania = new List<Danie>();
            this.Zamowienia = new Dictionary<string, Zamowienie>();
            this.KategorieDan = new List<string>();
        }

        // Dodanie konta klienta
        public void DodajKontoKlienta(string imie, string nazwisko, string email, string nrTel, string haslo)
        {
            Klient nowyKlient = new Klient(imie, nazwisko, email, nrTel);
            Konto noweKonto = new Konto(nowyKlient, haslo);

            // Sprawdzanie, czy konto z tym loginem (emailem) już istnieje
            if (konta.Any(k => k.Login == noweKonto.Login))
            {
                throw new ArgumentException("Konto o podanym loginie jest już zarejestrowane!");
            }

            // Dodanie konta do listy
            konta.Add(noweKonto);
            Console.WriteLine("Konto klienta zostało pomyślnie utworzone.");
        }

        // Dodanie konta pracownika
        public void DodajKontoPracownika(string imie, string nazwisko, string email, string nrTel, string pozycja, bool czyKucharz, string pesel, string haslo, EnumUprawienia uprawienia, string login)
        {
            Pracownik nowyPracownik = new Pracownik(pozycja, czyKucharz, pesel, imie, nazwisko, email, nrTel);
            Konto noweKonto = new Konto(nowyPracownik, haslo, uprawienia, login);

            // Sprawdzanie, czy konto z tym loginem (emailem) już istnieje
            if (konta.Any(k => k.Login == noweKonto.Login))
            {
                throw new ArgumentException("Konto o podanym emailu jest już zarejestrowane!");
            }

            // Dodanie konta do listy
            konta.Add(noweKonto);

            // Dodanie pracownika do listy pracowników
            pracownicy.Add(nowyPracownik);

            Console.WriteLine("Konto pracownika zostało pomyślnie utworzone.");
        }

        // Metoda zwracająca listę klientów
        public List<Klient> PobierzListeKlientow()
        {
            return konta
                .Where(k => k.Wlasciciel is Klient) // Filtrujemy tylko konta klientów
                .Select(k => (Klient)k.Wlasciciel) // Rzutujemy Wlasciciel na Klient
                .ToList();
        }

        // Usuwanie pracownika po peselu
        public void UsunPracownika(string pesel)
        {
            var pracownikDoUsuniecia = pracownicy.FirstOrDefault(p => p.Pesel == pesel);
            if (pracownikDoUsuniecia == null)
            {
                throw new ArgumentException("Pracownik o podanym peselu nie istnieje.");
            }

            pracownicy.Remove(pracownikDoUsuniecia);

            // Usunięcie powiązanego konta
            var kontoDoUsuniecia = konta.FirstOrDefault(k => k.Wlasciciel is Pracownik && ((Pracownik)k.Wlasciciel).Pesel == pesel);
            if (kontoDoUsuniecia != null)
            {
                konta.Remove(kontoDoUsuniecia);
            }

            Console.WriteLine($"Pracownik o peselu {pesel} został usunięty.");
        }

        // Usuwanie klienta po emailu
        public void UsunKlienta(string email)
        {
            var kontoDoUsuniecia = konta.FirstOrDefault(k => k.Wlasciciel is Klient && k.Login == email);
            if (kontoDoUsuniecia == null)
            {
                throw new ArgumentException("Klient o podanym emailu nie istnieje.");
            }

            konta.Remove(kontoDoUsuniecia);

            Console.WriteLine($"Klient o emailu {email} został usunięty.");
        }

        // Edytowanie pracownika po peselu
        public void EdytujPracownika(string pesel, Pracownik nowyPracownik)
        {
            var pracownikDoEdycji = pracownicy.FirstOrDefault(p => p.Pesel == pesel);
            if (pracownikDoEdycji == null)
            {
                throw new ArgumentException("Pracownik o podanym peselu nie istnieje.");
            }

            pracownicy.Remove(pracownikDoEdycji);
            pracownicy.Add(nowyPracownik);

            // Edytowanie powiązanego konta
            var kontoDoEdycji = konta.FirstOrDefault(k => k.Wlasciciel is Pracownik && ((Pracownik)k.Wlasciciel).Pesel == pesel);
            if (kontoDoEdycji != null)
            {
                konta.Remove(kontoDoEdycji);
                konta.Add(new Konto(nowyPracownik, kontoDoEdycji.Haslo, kontoDoEdycji.Uprawienia, kontoDoEdycji.Login));
            }

            Console.WriteLine($"Pracownik o peselu {pesel} został zaktualizowany.");
        }

        // Edytowanie klienta po emailu
        public void EdytujKlienta(string email, Klient nowyKlient)
        {
            var kontoDoEdycji = konta.FirstOrDefault(k => k.Wlasciciel is Klient && k.Login == email);
            if (kontoDoEdycji == null)
            {
                throw new ArgumentException("Klient o podanym emailu nie istnieje.");
            }

            konta.Remove(kontoDoEdycji);
            konta.Add(new Konto(nowyKlient, kontoDoEdycji.Haslo));

            Console.WriteLine($"Klient o emailu {email} został zaktualizowany.");
        }
    }
}

