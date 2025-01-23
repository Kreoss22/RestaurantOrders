using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    [DataContract]
    [KnownType(typeof(Danie))]
    public class Danie
    {
        [DataMember]
        private string nazwa;
        [DataMember]
        private string kategoria;
        [DataMember]
        private decimal cena;

        public Danie(string nazwa, string kategoria, List<string> składniki, decimal cena)
        {
            this.nazwa = nazwa;
            this.kategoria = kategoria;
            this.cena = cena;
        }

        public override string ToString()
        {
            return $"{this.nazwa} {this.kategoria} - {this.cena}";
        }
    }
}
