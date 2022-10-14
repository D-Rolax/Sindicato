using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Models.Request
{
    public class ComunidadRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class GrupoRequest
    {
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class Rutas
    {
        public double Latitud { get; set; }
        public double Longidud { get; set; }
    }
}
