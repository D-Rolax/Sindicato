using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.common.Models.Response
{
    public class RutasDetailsRequest
    {
        public int IdComunidad { get; set; }
        public int IdGrupo { get; set; }
        public List<RutasDetailRequest> Rutas { get; set; }
    }
}
