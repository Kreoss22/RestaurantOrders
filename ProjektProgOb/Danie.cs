using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Danie
    {
        private string nazwa;
        private string kategoria;
        private List<string> składniki;
        private decimal cena;

        public Danie(string nazwa, string kategoria, List<string> składniki, decimal cena)
        {
            this.nazwa = nazwa;
            this.kategoria = kategoria;
            this.składniki = składniki;
            this.cena = cena;
        }
    }
}
