using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class Afiliados
    {
        public Afiliados()
        {
            AsignacionHorarioChofer = new HashSet<AsignacionHorarioChofer>();
        }

        public int Id { get; set; }
        public int? TipoVehiculoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Ci { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }

        public virtual TiposVehiculos TipoVehiculo { get; set; }
        public virtual ICollection<AsignacionHorarioChofer> AsignacionHorarioChofer { get; set; }
    }
}
