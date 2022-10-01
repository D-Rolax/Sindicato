using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models.Response;
using WSSindicato.Services;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliadoController : ControllerBase
    {
        private readonly IAfiliadoService _afiliadoService;

        public AfiliadoController(IAfiliadoService afiliadoService)
        {
            _afiliadoService = afiliadoService;
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                _afiliadoService.delete(Id);
                respuesta.Exito = 1;

            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }
            return Ok(respuesta);
        }
    }
}
