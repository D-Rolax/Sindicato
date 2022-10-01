using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class TiposVehiculos
    {
        public TiposVehiculos()
        {
            Afiliados = new HashSet<Afiliados>();
        }
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<Afiliados> Afiliados { get; set; }
    }
}
