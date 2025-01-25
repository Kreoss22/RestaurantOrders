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
        void EdytujKlienta(string email, Klient nowyKlient);
        void EdytujPracownika(string pesel, Pracownik nowyPracownik);
    }

    [DataContract]
    public class Restauracja : IRestauracja
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
        public virtual List<Konto> Konta { get => konta; set => konta = value; }
        public virtual List<Pracownik> Pracownicy { get => pracownicy; set => pracownicy = value; }
        public virtual Dictionary<string, Zamowienie> Zamowienia { get => zamowienia; set => zamowienia = value; }
        public virtual List<Danie> Dania { get => dania; set => dania = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }
        public string Domena { get => domena; set => domena = value; }

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

        // Dodanie konta klienta
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
        // Dodanie konta pracownika

        public void DodajDanie(Danie danie)
        {
            if (Dania.Any(k => k.Nazwa == danie.Nazwa))
            {
                throw new ArgumentException("Danie o podanej nazwie jest już zarejestrowane!");
            }
            
            dania.Add(danie);
            
        }

        public void DodajZamowienieKlienta(Zamowienie zamowienie, string login)
        {
            if (zamowienia.ContainsKey(zamowienie.IdZamowienia))
            {
                throw new ArgumentException("Zamowienia o danym id już istnieje!");
            }
            zamowienia.Add(zamowienie.IdZamowienia, zamowienie);
            Klient znalezionyKlient = (Klient)konta.FirstOrDefault(k => k.Login == login && k.Wlasciciel is Klient).Wlasciciel;
            if(znalezionyKlient != null)
            {
                znalezionyKlient.ListaZamowien.Add(zamowienie); 
            }
        }

        public void DodajZamowienieLokalne(Zamowienie zamowienie)
        {
            if (zamowienia.ContainsKey(zamowienie.IdZamowienia))
            {
                throw new ArgumentException("Zamowienia o danym id już istnieje!");
            }
            zamowienia.Add(zamowienie.IdZamowienia, zamowienie);
        }

        public void ZmienStatusZamowienia(string idZamowienia, EnumStanZamowienia stanZamowienia)
        {
            zamowienia[idZamowienia].StanZamowienia = stanZamowienia;
        }

        // Metoda zwracająca listę klientów
        public virtual List<Klient> PobierzListeKlientow()
        {
            return konta
                .Where(k => k.Wlasciciel is Klient) // Filtrujemy tylko konta klientów
                .Select(k => (Klient)k.Wlasciciel) // Rzutujemy Wlasciciel na Klient
                .ToList();
        }
      
        // Usuwanie pracownika po peselu
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

        public void UsunDanie(string nazwa)
        {
            Danie danieDoUsuniecia = dania.FirstOrDefault(d => d.Nazwa == nazwa);
            if (danieDoUsuniecia == null)
            {
                throw new ArgumentException("Dane danie nie istnieje");
            }

            dania.Remove(danieDoUsuniecia);
        }
        
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

        public void SortujKonta()
        {
            Konta.Sort();
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
        public static Restauracja OdczytajXml(string nazwa)
        {
            if (!File.Exists(nazwa)) { return null; }
            DataContractSerializer dsc = new DataContractSerializer(typeof(Restauracja), new List<Type> { typeof(Klient), typeof(Konto), typeof(Zamowienie), typeof(Danie), typeof(Pracownik) });
            using (XmlReader reader = XmlReader.Create(nazwa))
                return (Restauracja)dsc.ReadObject(reader);
        }
    }

}


