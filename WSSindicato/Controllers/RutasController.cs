using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Response;
using WSSindicato.Services.HorarioViajes;

namespace WSSindicato.Controllers
{
    [Autorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RutasController : ControllerBase
    {
        private readonly IRutasService _rutasService;
        private readonly SindicatoContext _db;

        public RutasController(IRutasService rutasService, SindicatoContext db)
        {
            _rutasService = rutasService ?? throw new ArgumentException(nameof(rutasService));
            _db = db;
        }
        //[HttpGet]
        //public async Task<List<RutasRequest>> get()
        //{
        //    return await _rutasService.getRutas();
        //    //Respuesta res = new Respuesta();
        //    //try
        //    //{
        //    //    var rutas = await _rutasService.getRutas();
        //    //    res.Exito = 1;
        //    //    res.Data = rutas;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    res.Mensaje = ex.Message;
        //    //}
        //    //return Ok(res);
        //}
        [HttpPut("{rutas}")]
        public IActionResult getrutas([FromBody] RutasRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Respuesta res = new Respuesta();
            try
            {
                var rutas = _db.Rutas.Where(x => x.ComunidadId == model.IdComunidad && x.GrupoId == model.IdGrupo)
                    .ToList();
                res.Exito = 1;
                res.Data = rutas;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            Respuesta res = new Respuesta();
            try
            {
                //var list = _db.Grupos.Include(g => g.Rutas);
                //var list2 = _db.Comunidades.Include(c => c.Rutas);
                //var list3 = list.Include(list2).ToListAsync();
                //return await _rutasservice.getrutas();
                var grupos_comuidad = await (from r in _db.Rutas
                                             join c in _db.Comunidades
                                             on r.ComunidadId equals c.Id
                                             join g in _db.Grupos
                                             on r.GrupoId equals g.Id
                                             //group r by new { idGrupo = g.Id, idComunidad = c.Id, nombreGrupo = g.Nombre,
                                             //    nombreComunidad = c.Nombre,g.Estado,r.ComunidadId,r.GrupoId} into gr
                                             //select new
                                             //{
                                             //    IdGrupo = gr.Key.idGrupo,
                                             //    IdComunidad = gr.Key.idComunidad,
                                             //    NombreGrupo = gr.Key.nombreGrupo,
                                             //    NombreComunidad = gr.Key.nombreComunidad,
                                             //    Estado = gr.Key.Estado
                                             //}
                                             select new
                                             {
                                                 IdGrupo = g.Id,
                                                 IdComunidad = c.Id,
                                                 NombreGrupo = g.Nombre,
                                                 NombreComunidad = c.Nombre,
                                                 Estado = g.Estado,
                                                 Rutas = g.Rutas.Select(x => new
                                                 {
                                                     Latitud = x.Latitud,
                                                     Longitud = x.Longitud
                                                 })
                                             }
                                             ).ToListAsync();//new { idGrupo = x.IdGrupo, IdComunidad = x.IdComunidad, NombreGrupo = x.NombreComunidad, nombrecomunidad = x.NombreComunidad, estado = x.Estado, ruta = x.Rutas }).ToListAsync();
                                                             //Select(x => new { x.Key, count = x.Count() }).ToDictionaryAsync(x => x.Key, x => x.count);
                var agrupar = from db in grupos_comuidad group db by new { db.IdGrupo, db.IdComunidad, db.NombreGrupo, db.NombreComunidad, db.Estado } into g select g.Key;
                var grupos_comuidad_rutas = from db in agrupar select new { db.IdGrupo, db.IdComunidad, db.NombreGrupo, db.NombreComunidad, db.Estado, Rutas = _db.Rutas.Where(x => x.ComunidadId == db.IdComunidad && x.GrupoId == db.IdGrupo) };
                res.Exito = 1;
                res.Data = grupos_comuidad_rutas;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Add(RutasRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                _rutasService.AddRuta(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {

                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpPost("{delete}")]
        public async Task delete([FromBody] RutasRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                await _rutasService.delete(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {

                res.Mensaje = ex.Message;
            }
        }
    }

    internal class AutorizeAttribute : Attribute
    {
    }
}
