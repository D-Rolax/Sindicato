using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models.Request;

namespace WSSindicato.Services.GruposComunidad
{
    public interface IGrupoService
    {
        public void Add(GrupoRequest model);
        public void Get();
        public void Edit(GrupoRequest model);
        public void delete(int Id);
    }
}
