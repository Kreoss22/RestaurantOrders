using Restaurant;

namespace TestRestauracja
{
    [TestClass]
    public class TestRestauracja
    {
        [TestMethod]
        public void TestPeselException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Pracownik("a", false, "zlypesel", "b", "c", "d", "e"));          
        }
        [TestMethod]
        public void TestEmailException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a","b","zlyemail","123123123"));
        }
        [TestMethod]
        public void TestTelefonException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Klient("a", "b", "dobryemail@gmail.com", "zlytelefon"));
        }
        [TestMethod]
        public void TestSerializacjaDoXml()
        {
            Restauracja r = new("abc", "efg");
            r.DodajKontoKlienta("abc", "abc", "a@gmail.com", "123123123", "c");
            Assert.IsTrue(r.ZapiszXML("test"));
        }
        [TestMethod]
        public void TestPowtorkaEmail()
        {
            Restauracja r = new("abc", "efg");
            r.DodajKontoKlienta("abc", "abc", "a@gmail.com", "123123123", "c");
            Assert.ThrowsException<ArgumentException>(() => r.DodajKontoKlienta("a", "b", "a@gmail.com", "123123123","c"));
        }
    }

}
