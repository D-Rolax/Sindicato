using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Services.HorarioViajes
{
    public interface IRutasService
    {
        public void AddRuta(RutasResponse model);
        public Task delete(RutasResponse model);
        public void cambioEstado();
        public Task<List<RutasResponse>> getRutas();
    }
}
