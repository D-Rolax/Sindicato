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
        [HttpGet]
        public async Task<List<RutasResponse>> get()
        {
            return await _rutasService.getRutas();
            //Respuesta res = new Respuesta();
            //try
            //{
            //    var rutas = await _rutasService.getRutas();
            //    res.Exito = 1;
            //    res.Data = rutas;
            //}
            //catch (Exception ex)
            //{
            //    res.Mensaje = ex.Message;
            //}
            //return Ok(res);
        }
        [HttpGet("{detallerutas}")]
        public IActionResult getrutas([FromBody] RutasResponse model)
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
        //[HttpGet]
        //public async Task<IActionResult> get()
        //{
        //    Respuesta res = new Respuesta();
        //    try
        //    {
        //        var list = await _db.grupos.include(x => x.rutas).tolistasync();
        //        return await _rutasservice.getrutas();
        //        var grupos_comuidad = await (from rutas in _db.rutas
        //                                     join comunidad in _db.comunidades
        //                                     on rutas.comunidadid equals comunidad.id
        //                                     join grupos in _db.grupos
        //                                     on rutas.grupoid equals grupos.id
        //                                     select new
        //                                     {
        //                                         grupos.id,
        //                                         grupos.nombre,
        //                                         idcomunidad = comunidad.id,
        //                                         nombrecomunidad = comunidad.nombre

        //                                     }).groupby(x => new { id = x.id, nombre = x.nombre, idcomunidad = x.idcomunidad, nombrecomunidad = x.nombrecomunidad }).
        //                                     select(x => new { x.key, count = x.count() }).todictionaryasync(x => x.key, x => x.count);


        //        res.Exito = 1;
        //        res.Data = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Mensaje = ex.Message;
        //    }
        //    return Ok(res);
        //}
        [HttpPost]
        public IActionResult Add(RutasResponse model)
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
        public async Task delete([FromBody] RutasResponse model)
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
