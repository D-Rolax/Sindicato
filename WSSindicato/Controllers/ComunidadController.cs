using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;
using WSSindicato.Services.GruposComunidad;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunidadController : ControllerBase
    {
        private readonly IComunidadService _comunidadService;
        private readonly SindicatoContext db;
        Respuesta res = new Respuesta();

        public ComunidadController(IComunidadService comunidadService,SindicatoContext db)
        {
            _comunidadService = comunidadService;
            this.db = db;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                    //var lst = db.TiposVehiculos.OrderByDescending(d => d.Id).ToList();
                    var lst = db.Comunidades.OrderByDescending(d => d.Id).ToList();
                    res.Exito = 1;
                    res.Data = lst;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Add(ComunidadRequest model)
        {
            try
            {
                _comunidadService.Add(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpPut] 
        public IActionResult edit(ComunidadRequest model)
        {
            try
            {
                _comunidadService.Edit(model);
                res.Exito = 1;

            }
            catch (Exception ex)
            {

                res.Mensaje=ex.Message;
            }
            return Ok(res);
        }
        [HttpDelete("{Id}")]
        public IActionResult delete(int Id)
        {
            try
            {
                _comunidadService.delete(Id);
                res.Exito = 1;
            }
            catch (Exception ex)
            {

                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    }
}
