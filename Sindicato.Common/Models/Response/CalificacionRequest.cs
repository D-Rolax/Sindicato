using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class CalificacionRequest
    {
        public int IdChofer { get; set; }
        public double Puntaje { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
