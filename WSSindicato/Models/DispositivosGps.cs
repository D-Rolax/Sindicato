using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class DispositivosGps
    {
        public DispositivosGps()
        {
            Trakings = new HashSet<Trakings>();
        }

        public int Id { get; set; }
        public int DireccionIp { get; set; }
        public string NombreDispositivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Trakings> Trakings { get; set; }
    }
}
