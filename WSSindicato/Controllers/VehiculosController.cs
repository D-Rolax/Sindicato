using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;

namespace WSSindicato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta res = new Respuesta();
            try
            {
                using(SindicatoContext db=new SindicatoContext())
                {
                    var lst = db.TiposVehiculos.ToList();
                    res.Exito = 1;
                    res.Data = lst;
                }
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
                using(SindicatoContext db=new SindicatoContext())
                {
                    TiposVehiculos tipVehiculo = new TiposVehiculos();
                    tipVehiculo.Placa = model.Placa;
                    tipVehiculo.Modelo = model.Modelo;
                    tipVehiculo.Tipo = model.Tipo;
                    tipVehiculo.Marca = model.Marca;
                    tipVehiculo.Color = model.Color;
                    tipVehiculo.Estado = "Activo";
                    tipVehiculo.Fecha = DateTime.Now.Date;
                    db.TiposVehiculos.Add(tipVehiculo);
                    db.SaveChanges();
                    res.Exito = 1;
                }
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
                using (SindicatoContext db = new SindicatoContext())
                {
                    TiposVehiculos tipVehiculo = db.TiposVehiculos.Find(model.Id);
                    tipVehiculo.Placa = model.Placa;
                    tipVehiculo.Modelo = model.Modelo;
                    tipVehiculo.Tipo = model.Tipo;
                    tipVehiculo.Marca = model.Marca;
                    tipVehiculo.Color = model.Color;
                    tipVehiculo.Estado = model.Estado;
                    tipVehiculo.Fecha = DateTime.Now.Date;
                    db.Entry(tipVehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    res.Exito = 1;
                }
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
                using (SindicatoContext db = new SindicatoContext())
                {
                    TiposVehiculos tipVehiculos = db.TiposVehiculos.Find(Id);
                    db.Remove(tipVehiculos);
                    db.SaveChanges();
                    res.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    }
}
