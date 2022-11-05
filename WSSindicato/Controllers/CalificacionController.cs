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
    public class CalificacionController : ControllerBase
    {
        private readonly SindicatoContext _db;

        public CalificacionController(SindicatoContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult post(CalificacionRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                var calificacion = new Calificacion();
                calificacion.ChoferId = model.IdChofer;
                calificacion.Puntaje = model.Puntaje;
                calificacion.Descripcion = model.Descripcion;
                calificacion.Fecha = DateTime.Now.Date;
                calificacion.Estado = "Activo";
                _db.Calificacion.Add(calificacion);
                _db.SaveChanges();
                res.Exito = 1;
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
