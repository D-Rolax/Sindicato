using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models.Request;

namespace WSSindicato.Services.GruposComunidad
{
    public interface IComunidadService
    {
        public void Add(ComunidadRequest model);
        public void Get();
        public void Edit(ComunidadRequest model);
        public void delete(int Id);
    }
}
