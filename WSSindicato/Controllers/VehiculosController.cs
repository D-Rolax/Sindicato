using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
    [Authorize]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly SindicatoContext db;

        public VehiculosController(IVehiculoService vehiculoService, SindicatoContext db)
        {
            _vehiculoService = vehiculoService;
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Respuesta res = new Respuesta();
            try
            {
                    //var lst = db.TiposVehiculos.OrderByDescending(d => d.Id).ToList();
                    var lst = await db.TiposVehiculos
                    .Include(d => d.Afiliados)
                    .OrderByDescending(d => d.Id).ToListAsync();
                    res.Exito = 1;
                    res.Data = lst;
            }
            catch (Exception ex)
            {
                res.Mensaje=ex.Message;
            }
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Add(VehiculosRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                _vehiculoService.Add(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Mensaje=ex.Message;
            }
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Edit(VehiculosRequest model)
        {
            Respuesta res = new Respuesta();
            try
            {
                _vehiculoService.Edit(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta res = new Respuesta();
            try
            {

            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    }
}
