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
        string nazwa;
        EnumKategorieDan kategoria;
        List<string> składniki;


    }
}
