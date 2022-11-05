using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Response;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly SindicatoContext _db;

        public HorarioController(SindicatoContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult post(HorarioRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                var horario = new Horarios();
                horario.HodaSalida = model.HorarioSalida;
                horario.HoraLlegada = model.HoraioLlegada;
                horario.Estado = model.Estado;
                horario.FechaRegistro = DateTime.Now.Date;
                _db.Horarios.Add(horario);
                _db.SaveChanges();
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
            Respuesta res = new Respuesta();
            try
            {
                var horarios = _db.Horarios.ToList();
                res.Exito = 1;
                res.Data = horarios;
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
