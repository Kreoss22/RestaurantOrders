﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Restauracja
    {
        Dictionary<string, Zamowienie> zamowienia;
        List<Danie> dania;
        private Stack<Konto> listaKont;
    }
}