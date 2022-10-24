using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models
{
    public class PruebaDefinitiva
    {
        public object Grupos { get; set; }
        public object Comunidades { get; set; }
        public List<ListaFefinitiva> Lista { get; set; }

    }
    public class ListaFefinitiva
    {
        public double Latitud { get; set; }
        public int Lingitd { get; set; }
    }
}
