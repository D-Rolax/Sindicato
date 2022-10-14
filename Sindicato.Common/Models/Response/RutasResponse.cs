using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class RutasResponse
    {
        public int IdComunidad { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreComunidad { get; set; }
        public string Estado { get; set; }
        public List<Ruta> Rutas { get; set; }
    }
    public class Ruta
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
