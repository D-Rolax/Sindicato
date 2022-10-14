using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class UserResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public List<DatosUsuario> Data{ get; set; }
    }
    public class DatosUsuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
    }
}
