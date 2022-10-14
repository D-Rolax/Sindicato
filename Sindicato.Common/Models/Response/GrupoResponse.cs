using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class GrupoResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Grupos> Data{ get; set; }
    }
    public class Grupos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
