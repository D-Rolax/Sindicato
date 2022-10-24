using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class RutasResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public UnasolaRuta Data { get; set; }
    }
    public class UnasolaRuta
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
