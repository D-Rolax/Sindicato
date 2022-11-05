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
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;
        private readonly SindicatoContext db;
        Respuesta res = new Respuesta();

        public GrupoController(IGrupoService grupoService,SindicatoContext db)
        {
            _grupoService = grupoService;
            this.db = db;
        }
        [HttpPost]
        public IActionResult post(GrupoRequest model)
        {
            try
            {
                _grupoService.Add(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpGet]
        public IActionResult get()
        {
            try
            {
                var list = db.Grupos.OrderByDescending(d => d.Id).ToList();
                res.Data = list;
                res.Exito = 1;
            }
            catch (Exception ex)
            {

                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpPut]
        public IActionResult put(GrupoRequest model)
        {
            try
            {
                _grupoService.Edit(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {

                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpDelete("{Id}")]
        public IActionResult delete(int Id)
        {
            try
            {
                _grupoService.delete(Id);
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
