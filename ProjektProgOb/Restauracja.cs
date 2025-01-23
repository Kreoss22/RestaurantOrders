using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Restaurant
{
    [DataContract]
    public class Restauracja
    {
        [DataMember (Order =1)]
        private string domena;
        [DataMember(Order =2)]
        private string nazwa;
        [DataMember(Order =5)]
        Dictionary<string, Zamowienie> zamowienia; // klucz - id zamowienia
        [DataMember(Order =6)]
        List<Danie> dania;
        [DataMember(Order= 4)]
        private List<Konto> konta;
        [DataMember(Order =3)]
        private List<Pracownik> pracownicy;
        [DataMember(Order =7)]
        private List<string> kategorieDan;
        

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
                throw new ArgumentException("Konto o podanym emailu jest już zarejestrowane!");
            }

            // Dodanie konta do listy
            konta.Add(noweKonto);
            Console.WriteLine("Konto klienta zostało pomyślnie utworzone.");
        }

        // Dodanie konta pracownika
        public void DodajKontoPracownika(string imie, string nazwisko, string email, string nrTel, string pozycja, bool czyKucharz, string pesel, string haslo, EnumUprawienia uprawienia)
        {
            Pracownik nowyPracownik = new Pracownik(pozycja, czyKucharz, pesel, imie, nazwisko, email, nrTel);
            Konto noweKonto = new Konto(nowyPracownik, haslo, uprawienia);

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
        public bool ZapiszXML(string nazwa)
        {
            if (konta == null || konta.Count == 0) // Sprawdzamy czy restauracja zawiera dane
            {
                Console.WriteLine("Brak danych"); 
                return false;
            }

            try
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(Restauracja));
                using (XmlTextWriter writer = new XmlTextWriter(nazwa, Encoding.UTF8))
                {
                    dcs.WriteObject(writer, this);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blad serializacji {ex.Message}"); //komunikat bledu serializacji
                return false;
            }
        }
        
        /* //Nie Działa
        public static Restauracja OdczytajXml(string nazwa)
        {
            if (!File.Exists(nazwa)) { return null; }
            DataContractSerializer dsc = new DataContractSerializer(typeof(Restauracja), new List<Type> { typeof(Klient), typeof(Konto) });
            using (XmlReader reader = XmlReader.Create(nazwa))
            return (Restauracja)dsc.ReadObject(reader);
        } */
    }
}
