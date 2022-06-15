using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AfiliadosController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            Respuesta res = new Respuesta();
            res.Exito = 0;
            try
            {
                using (SindicatoContext db = new SindicatoContext())
                {
                    var lst = db.Afiliados.ToList();
                    res.Exito = 1;
                    res.Data = lst;

                }
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message ;
            }
            return Ok(res);
        }
        public IActionResult Add()
        {
            return Ok();
        }
    }
}
