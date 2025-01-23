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
        public void TestEmailException() //sprawdza czy poprawnie filtrowany jest email
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a","b","zlyemail","123123123"));
        }
        [TestMethod]
        public void TestTelefonException() //sprawdza czy poprawnie filtrowany jest nr telefonu
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a", "b", "dobryemail@gmail.com", "zlytelefon"));
        }
        [TestMethod]
        public void TestSerializacjaDoXml() //sprawdza tworzenie pliku xml
        {
            Restauracja r = new("abc", "efg");
            r.DodajKontoKlienta("abc", "abc", "a@gmail.com", "123123123", "c");
            Assert.IsTrue(r.ZapiszXML("test"));
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
