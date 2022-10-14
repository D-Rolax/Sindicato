using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;
using WSSindicato.Services;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly SindicatoContext _db;

        public UserController(IUserService userService, SindicatoContext db)
        {
            _userService = userService;
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userResponse;

            return Ok(respuesta);
        }
        [Authorize]
        [HttpPost]
        public IActionResult GetUserByEmail([FromBody] TokenResponse model)
        {
            Respuesta res = new Respuesta();
            try
            { 
                 var usuario = _db.Usuario.
                               Where(b => b.Email==model.Email);
                res.Exito = 1;
                res.Data = usuario;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                
            }
            return Ok(res);
        }
    }
}
