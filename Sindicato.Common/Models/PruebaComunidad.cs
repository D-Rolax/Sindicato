using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models
{
    public class PruebaComunidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public List<PruebaRutas> Rutas { get; set; }
    }
}
