using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class Castigos
    {
        public int Id { get; set; }
        public int? ChoferId { get; set; }
        public string Descripcion { get; set; }
        public string Sancion { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

        public virtual AsignacionHorarioChofer Chofer { get; set; }
    }
}
