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

        public string Nazwa { get => nazwa; set => nazwa = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }
        public decimal Cena { get => cena; set => cena = value; }


        public Danie()
        {
            Nazwa = string.Empty;
            Kategoria = string.Empty;
            Cena = 0;
        }
        public Danie(string nazwa, string kategoria, decimal cena)
        {
            this.Nazwa = nazwa;
            this.Kategoria = kategoria;
            this.Cena = cena;
        }

        public override string ToString()
        {
            return $"{this.Nazwa} {this.Kategoria} - {this.Cena}";
        }
    }
}
