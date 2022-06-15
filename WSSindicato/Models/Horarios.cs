using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class Horarios
    {
        public Horarios()
        {
            HorariosPorRutas = new HashSet<HorariosPorRutas>();
        }

        public int Id { get; set; }
        public DateTime HodaSalida { get; set; }
        public DateTime HoraLlegada { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<HorariosPorRutas> HorariosPorRutas { get; set; }
    }
}
