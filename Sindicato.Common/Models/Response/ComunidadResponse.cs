using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class ComunidadResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Comunidades> Data { get; set; }
    }
    public class Comunidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
