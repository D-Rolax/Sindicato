using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Response;
using WSSindicato.Services;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrakingController : ControllerBase
    {
        private readonly SindicatoContext _db;

        public TrakingController(SindicatoContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult post(RutasRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                var List = (from t in _db.Rutas
                                  orderby t.Id descending
                                  select new {t.Id,t.GrupoId,t.ComunidadId, t.Latitud,t.Longitud}
                                  ).Where(x=>x.GrupoId==model.IdGrupo && x.ComunidadId==model.IdComunidad).First();
                res.Exito = 1;
                res.Data = List;
            }
            catch (Exception ex)
            {
                res.Exito = 0;
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    }
}
