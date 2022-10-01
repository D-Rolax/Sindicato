using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models.Request;

namespace WSSindicato.Services
{
    public interface IVehiculoService
    {
        public void Add(VehiculosRequest model);
        public void Edit(VehiculosRequest model);
    }
}
