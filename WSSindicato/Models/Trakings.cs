using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class Trakings
    {
        public int Id { get; set; }
        public int? HorarioChoferId { get; set; }
        public int? DispositivoGpsid { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public virtual DispositivosGps DispositivoGps { get; set; }
        public virtual AsignacionHorarioChofer HorarioChofer { get; set; }
    }
}
