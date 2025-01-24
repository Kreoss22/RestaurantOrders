using Restaurant;

namespace TestRestauracja
{
    [TestClass]
    public class TestRestauracja
    {
        [TestMethod]
        public void TestPeselException() //sprawdza czy poprawnie filtrowany jest pesel
        {
            Assert.ThrowsException<ArgumentException>(() => new Pracownik("a", false, "zlypesel", "b", "c", "d", "e"));          
        }
        [TestMethod]
        public void TestEmailException() //sprawdza czy poprawnie filtrowany jest adres email
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a","b","zlyemail","123123123"));
        }
        [TestMethod]
        public void TestTelefonException() //sprawdza czy poprawnie filtrowany jest nr telefonu
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a", "b", "dobryemail@gmail.com", "zlytelefon"));
        }
        [TestMethod]
        public void TestSerializacjaDoXml() //sprawdza tworzenie pliku xml i jego zawartosc
        {
            Restauracja r = new("abc", "efg");
            r.DodajKontoKlienta("abc", "abc", "a@gmail.com", "123123123", "c");
            r.ZapiszXML("test");
            Assert.IsTrue(File.Exists("./test"));
            string Xml = File.ReadAllText("./test");
            Assert.IsTrue(Xml.Contains("<imie>abc</imie>"));
            Assert.IsTrue(Xml.Contains("<nazwisko>abc</nazwisko>"));
            Assert.IsTrue(Xml.Contains("<email>a@gmail.com</email>"));
            Assert.IsTrue(Xml.Contains("<nrTel>123123123</nrTel>"));
            Assert.IsTrue(Xml.Contains("<haslo>c</haslo>"));
        }
        [TestMethod]
        public void TestPowtorkaEmail() //sprawdza czy poprawnie odrzucane sa powtorzone adresy email
        {
            Restauracja r = new("abc", "efg");
            r.DodajKontoKlienta("abc", "abc", "a@gmail.com", "123123123", "c");
            Assert.ThrowsException<ArgumentException>(() => r.DodajKontoKlienta("a", "b", "a@gmail.com", "123123123","c"));
        }
    }

}
