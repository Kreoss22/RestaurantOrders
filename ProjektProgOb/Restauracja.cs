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
    /// <summary>
    /// Interfejs definiujący operacje na restauracji.
    /// </summary>
    interface IRestauracja
    {
        void DodajKonto(Konto noweKonto);
        void DodajPracownika(Pracownik pracownik);
        void DodajDanie(Danie danie);
        void DodajZamowienieKlienta(Zamowienie zamowienie, string login);
        void DodajZamowienieLokalne(Zamowienie zamowienie);
        void ZmienStatusZamowienia(string idZamowienia, EnumStanZamowienia stanZamowienia);
        List<Klient> PobierzListeKlientow();
        void UsunPracownika(string pesel);
        void UsunKonto(string email);
        void UsunDanie(string nazwa);
        void EdytujKonto(string login, Konto noweKonto);
        void EdytujPracownika(string pesel, Pracownik nowyPracownik);
        void EdytujDanie(string nazwa, Danie danie);
    }

    [DataContract]

    /// <summary>
    /// Klasa reprezentująca restaurację.
    /// </summary>
    public class Restauracja : IRestauracja
    {
        [DataMember(Order = 1)]
        private string domena;
        [DataMember(Order = 2)]
        private string nazwa;
        [DataMember(Order = 5)]
        Dictionary<string, Zamowienie> zamowienia; // klucz - id zamowienia
        [DataMember(Order = 6)]
        List<Danie> dania;
        [DataMember(Order = 4)]
        private List<Konto> konta;
        [DataMember(Order = 3)]
        private List<Pracownik> pracownicy;
        [DataMember(Order = 7)]
        private List<string> kategorieDan;

        public List<string> KategorieDan { get => kategorieDan; set => kategorieDan = value; }
        public virtual List<Konto> Konta { get => konta; set => konta = value; }
        public virtual List<Pracownik> Pracownicy { get => pracownicy; set => pracownicy = value; }
        public virtual Dictionary<string, Zamowienie> Zamowienia { get => zamowienia; set => zamowienia = value; }

        public virtual List<Danie> Dania { get => dania; set => dania = value; }

        public string Nazwa { get => nazwa; set => nazwa = value; }

        public string Domena { get => domena; set => domena = value; }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Restauracja"/>.
        /// </summary>
        /// <param name="domena">Domena restauracji.</param>
        /// <param name="nazwa">Nazwa restauracji.</param>
        /// <param name="kategorieDan">Lista kategorii dań.</param>
        public Restauracja(string domena, string nazwa, List<string> kategorieDan)
        {
            this.Domena = domena;
            this.Nazwa = nazwa;
            this.Pracownicy = new List<Pracownik>();
            this.Konta = new List<Konto>();
            this.Dania = new List<Danie>();
            this.Zamowienia = new Dictionary<string, Zamowienie>();
            this.KategorieDan = kategorieDan;
        }


        /// <summary>
        /// Dodaje nowe konto do restauracji.
        /// </summary>
        /// <param name="noweKonto">Nowe konto do dodania.</param>
        public void DodajKonto(Konto noweKonto)
        {
            // Sprawdzanie, czy konto z tym loginem (emailem) już istnieje
            if (konta.Any(k => k.Equals(noweKonto)))
            {
                throw new ArgumentException("Konto o podanym loginie jest już zarejestrowane!");
            }

            // Dodanie konta do listy
            konta.Add(noweKonto);
        }

        //Dodawanie pracownika
        /// <summary>
        /// Dodaje pracownika do restauracji.
        /// </summary>
        /// <param name="pracownik">Pracownik do dodania.</param>
        public void DodajPracownika(Pracownik pracownik)
        {
            if (pracownicy.Any(k => k.Pesel == pracownik.Pesel))
            {
                throw new ArgumentException("Już istnieje pracownik z tym peselem!");
            }
            if (pracownicy.Any(k => k.Email == pracownik.Email))
            {
                throw new ArgumentException("Już istnieje pracownik z tym emailem!");
            }

            pracownicy.Add(pracownik);

        }

        /// <summary>
        /// Dodaje nowe danie do listy dań restauracji.
        /// </summary>
        /// <param name="danie">Danie do dodania.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy danie o tej nazwie już istnieje.</exception>
        public void DodajDanie(Danie danie)
        {
            if (Dania.Any(k => k.Nazwa == danie.Nazwa))
            {
                throw new ArgumentException("Danie o podanej nazwie jest już zarejestrowane!");
            }

            dania.Add(danie);

        }

        /// <summary>
        /// Dodaje zamówienie klienta do listy zamówień.
        /// </summary>
        /// <param name="zamowienie">Zamówienie klienta.</param>
        /// <param name="login">Login klienta.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy zamówienie o danym ID już istnieje.</exception>
        public void DodajZamowienieKlienta(Zamowienie zamowienie, string login)
        {
            if (zamowienia.ContainsKey(zamowienie.IdZamowienia))
            {
                throw new ArgumentException("Zamowienia o danym id już istnieje!");
            }
            zamowienia.Add(zamowienie.IdZamowienia, zamowienie);
            Klient znalezionyKlient = (Klient)konta.FirstOrDefault(k => k.Login == login && k.Wlasciciel is Klient).Wlasciciel;
            if (znalezionyKlient != null)
            {
                znalezionyKlient.ListaZamowien.Add(zamowienie);
            }
        }

        /// <summary>
        /// Dodaje zamówienie lokalne do restauracji.
        /// </summary>
        /// <param name="zamowienie">Zamówienie do dodania.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy zamówienie o danym ID już istnieje.</exception>
        public void DodajZamowienieLokalne(Zamowienie zamowienie)
        {
            if (zamowienia.ContainsKey(zamowienie.IdZamowienia))
            {
                throw new ArgumentException("Zamowienia o danym id już istnieje!");
            }
            zamowienia.Add(zamowienie.IdZamowienia, zamowienie);
        }

        /// <summary>
        /// Zmienia status zamówienia na określony.
        /// </summary>
        /// <param name="idZamowienia">Identyfikator zamówienia.</param>
        /// <param name="stanZamowienia">Nowy stan zamówienia.</param>
        public void ZmienStatusZamowienia(string idZamowienia, EnumStanZamowienia stanZamowienia)
        {
            zamowienia[idZamowienia].StanZamowienia = stanZamowienia;
        }

        // Metoda zwracająca listę klientów
        /// <summary>
        /// Pobiera listę klientów restauracji.
        /// </summary>
        /// <returns>Lista klientów.</returns>
        public virtual List<Klient> PobierzListeKlientow()
        {
            return konta
                .Where(k => k.Wlasciciel is Klient) // Filtrujemy tylko konta klientów
                .Select(k => (Klient)k.Wlasciciel) // Rzutujemy Wlasciciel na Klient
                .ToList();
        }

        // Usuwanie pracownika po peselu
        /// <summary>
        /// Usuwa pracownika na podstawie numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL pracownika.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy pracownik nie istnieje.</exception>
        public void UsunPracownika(string pesel)
        {
            Pracownik pracownikDoUsuniecia = pracownicy.FirstOrDefault(p => p.Pesel == pesel);
            if (pracownikDoUsuniecia == null)
            {
                throw new ArgumentException("Dany pracownik nie istnieje");
            }

            pracownicy.Remove(pracownikDoUsuniecia);

            // Usunięcie powiązanego konta
            Konto kontoDoUsuniecia = konta.FirstOrDefault(k => k.Wlasciciel is Pracownik && ((Pracownik)k.Wlasciciel).Pesel == pesel);
            if (kontoDoUsuniecia != null)
            {
                konta.Remove(kontoDoUsuniecia);
            }
        }

        /// <summary>
        /// Usuwa danie na podstawie nazwy.
        /// </summary>
        /// <param name="nazwa">Nazwa dania do usunięcia.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy danie nie istnieje.</exception>
        public void UsunDanie(string nazwa)
        {
            Danie danieDoUsuniecia = dania.FirstOrDefault(d => d.Nazwa == nazwa);
            if (danieDoUsuniecia == null)
            {
                throw new ArgumentException("Dane danie nie istnieje");
            }

            dania.Remove(danieDoUsuniecia);
        }

        /// <summary>
        /// Usuwa konto na podstawie loginu.
        /// </summary>
        /// <param name="login">Login konta.</param>
        /// <exception cref="ArgumentException">Rzucane, gdy konto nie istnieje.</exception>
        public void UsunKonto(string login)
        {
            Konto kontoDoUsuniecia = konta.FirstOrDefault(k => k.Login == login);
            if (kontoDoUsuniecia == null)
            {
                throw new ArgumentException("Dany klient nie istnieje");
            }

            konta.Remove(kontoDoUsuniecia);
        }

        // Edytowanie pracownika po peselu
        /// <summary>
        /// Edytuje dane pracownika na podstawie numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL pracownika.</param>
        /// <param name="nowyPracownik">Nowe dane pracownika.</param>
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
        }


        public void EdytujKonto(string email, Konto noweKonto)
        {
            var kontoDoEdycji = konta.FirstOrDefault(k => k.Login == email);
            if (kontoDoEdycji == null)
            {
                throw new ArgumentException("Klient o podanym emailu nie istnieje.");
            }

            konta.Remove(kontoDoEdycji);
            konta.Add(noweKonto);
        }


        public void EdytujDanie(string nazwa, Danie noweDanie)
        {
            var danieDoEdycji = dania.FirstOrDefault(k => k.Nazwa == nazwa);
            if (danieDoEdycji == null)
            {
                throw new ArgumentException("Danie o podanej nazwie nie istnieje.");
            }

            dania.Remove(danieDoEdycji);
            dania.Add(noweDanie);
        }

        /// <summary>
        /// Sortuje konta użytkowników.
        /// </summary>
        public void SortujKonta()
        {
            Konta.Sort();
        }

        /// <summary>
        /// Zapisuje dane restauracji do pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku.</param>
        /// <returns>Zwraca true, jeśli zapis zakończył się sukcesem.</returns>
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

        /// <summary>
        /// Odczytuje dane restauracji z pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML.</param>
        /// <returns>Zwraca obiekt restauracji.</returns>
        public static Restauracja OdczytajXml(string nazwa)
        {
            if (!File.Exists(nazwa)) { return null; }
            DataContractSerializer dsc = new DataContractSerializer(typeof(Restauracja), new List<Type> { typeof(Klient), typeof(Konto), typeof(Zamowienie), typeof(Danie), typeof(Pracownik) });
            using (XmlReader reader = XmlReader.Create(nazwa))
                return (Restauracja)dsc.ReadObject(reader);
        }
    }

}


