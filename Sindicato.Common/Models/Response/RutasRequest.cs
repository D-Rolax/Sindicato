using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class RutasRequest
    {
        public int IdGrupo { get; set; }
        public int IdComunidad { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreComunidad { get; set; }
        public string Estado { get; set; }
        public List<RutasDetail> Rutas { get; set; }
    }
    public class RutasDetail
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
    public class ResponseRutasCompleto
    {
        public string Exito { get; set; }
        public string Mensaje { get; set; }
        public List<RutasRequest> Data { get; set; }
    }
}
