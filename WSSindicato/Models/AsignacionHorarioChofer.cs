using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class AsignacionHorarioChofer
    {
        public AsignacionHorarioChofer()
        {
            Calificacion = new HashSet<Calificacion>();
            Castigos = new HashSet<Castigos>();
            Trakings = new HashSet<Trakings>();
        }

        public int Id { get; set; }
        public int? AfiliadoId { get; set; }
        public int? HorarioRutaId { get; set; }
        public string Descripcion { get; set; }

        public virtual Afiliados Afiliado { get; set; }
        public virtual HorariosPorRutas HorarioRuta { get; set; }
        public virtual ICollection<Calificacion> Calificacion { get; set; }
        public virtual ICollection<Castigos> Castigos { get; set; }
        public virtual ICollection<Trakings> Trakings { get; set; }
    }
}
