using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class RutasDetailRequest
    {
        public int IdRuta { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
