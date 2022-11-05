using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class HorarioRequest
    {
        public DateTime HorarioSalida { get; set; }
        public DateTime HoraioLlegada { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
