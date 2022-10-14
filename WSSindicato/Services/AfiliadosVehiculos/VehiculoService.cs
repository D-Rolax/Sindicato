using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;

namespace WSSindicato.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly SindicatoContext db;

        public VehiculoService(SindicatoContext db)
        {
            this.db = db;
        }
        public void Add(VehiculosRequest model)
        {
            
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var tipvehiculo = new TiposVehiculos();
                        tipvehiculo.Placa = model.Placa;
                        tipvehiculo.Modelo = model.Modelo;
                        tipvehiculo.Tipo = model.Tipo;
                        tipvehiculo.Marca = model.Marca;
                        tipvehiculo.Color = model.Color;
                        tipvehiculo.Estado = "Activo";
                        tipvehiculo.Fecha = DateTime.Now.Date;
                        db.TiposVehiculos.Add(tipvehiculo);
                        db.SaveChanges();
                        foreach (var item in model.Afiliados)
                        {
                            var afiliados = new Models.Afiliados();
                            afiliados.Nombres = item.Nombres;
                            afiliados.Apellidos = item.Apellidos;
                            afiliados.Ci = item.Ci;
                            afiliados.Direccion = item.Direccion;
                            afiliados.FechaNacimiento = item.FechaNacimiento;
                            afiliados.TipoVehiculoId = tipvehiculo.Id;
                            afiliados.Estado = "Activo";
                            afiliados.FechaRegistro = DateTime.Now.Date;
                            db.Afiliados.Add(afiliados);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en la insercion");
                    }
                }
            
        }

        public void Edit(VehiculosRequest model)
        {
                TiposVehiculos tipVehiculo = db.TiposVehiculos.Include(a => a.Afiliados).FirstOrDefault(a => a.Id == model.Id);
                //TiposVehiculos tipVehiculo = db.TiposVehiculos.Find(model.Id);
                tipVehiculo.Placa = model.Placa;
                tipVehiculo.Modelo = model.Modelo;
                tipVehiculo.Tipo = model.Tipo;
                tipVehiculo.Marca = model.Marca;
                tipVehiculo.Color = model.Color;
                tipVehiculo.Estado = model.Estado;
                tipVehiculo.Fecha = DateTime.Now.Date;
                db.Entry(tipVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                foreach (var item in model.Afiliados)
                {
                    var ci = db.Afiliados.Any(x => x.Ci == item.Ci);
                    if (!ci)
                    {
                        var afiliados = new Afiliados();
                        afiliados.Nombres = item.Nombres;
                        afiliados.Apellidos = item.Apellidos;
                        afiliados.Ci = item.Ci;
                        afiliados.Direccion = item.Direccion;
                        afiliados.FechaNacimiento = item.FechaNacimiento;
                        afiliados.TipoVehiculoId = tipVehiculo.Id;
                        afiliados.Estado = "Activo";
                        afiliados.FechaRegistro = DateTime.Now.Date;
                        db.Afiliados.Add(afiliados);
                        db.SaveChanges();
                    }
                }
            
        }
    }
}
