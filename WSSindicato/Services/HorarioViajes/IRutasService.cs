using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Services.HorarioViajes
{
    public interface IRutasService
    {
        public void AddRuta(RutasRequest model);
        public Task delete(RutasRequest model);
        public void cambioEstado();
        public Task<List<RutasRequest>> getRutas();
    }
}
