using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    enum EnumKategorieDan
    {
        Przystawki,
        Pizza,
        Makarony,
        Glowne,
        Zimne,
        Gorace,
        Piwo,
        Wino,
        Mocne
    }
    internal class Danie
    {
        private string nazwa;
        private EnumKategorieDan kategoria;
        private List<string> składniki;
        private decimal cena;

    }
}
