using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class HorariosPorRutas
    {
        public HorariosPorRutas()
        {
            AsignacionHorarioChofer = new HashSet<AsignacionHorarioChofer>();
        }

        public int Id { get; set; }
        public int? RutaId { get; set; }
        public int? HorarioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public virtual Horarios Horario { get; set; }
        public virtual Rutas Ruta { get; set; }
        public virtual ICollection<AsignacionHorarioChofer> AsignacionHorarioChofer { get; set; }
    }
}
