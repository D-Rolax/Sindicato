using System;
using System.Collections.Generic;
using System.Text;
using WSSindicato.Models.Response;

namespace Sindicato.common.Models.Response
{
    public class LoginResponse
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public TokenResponse Data { get; set; }
    }
}
