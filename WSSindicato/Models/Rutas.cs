using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class Rutas
    {
        public Rutas()
        {
            HorariosPorRutas = new HashSet<HorariosPorRutas>();
        }

        public int Id { get; set; }
        public int? ComunidadId { get; set; }
        public int? GrupoId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public virtual Comunidades Comunidad { get; set; }
        public virtual Grupos Grupo { get; set; }
        public virtual ICollection<HorariosPorRutas> HorariosPorRutas { get; set; }
    }
}
