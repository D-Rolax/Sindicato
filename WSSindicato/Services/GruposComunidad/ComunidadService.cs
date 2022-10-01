using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;

namespace WSSindicato.Services.GruposComunidad
{
    public class ComunidadService : IComunidadService
    {
        private SindicatoContext _db;

        public ComunidadService(SindicatoContext db)
        {
            _db = db;
        }
        public void Add(ComunidadRequest model)
        {
                var comunidad = new Comunidades();
                comunidad.Nombre = model.Nombre;
                comunidad.Descripcion = model.Descripcion;
                comunidad.Estado = "Activo";
                comunidad.Fecha = DateTime.Now.Date;
                _db.Comunidades.Add(comunidad);
                _db.SaveChanges();
        }

        public void delete(int Id)
        {
                Comunidades tipVehiculos = _db.Comunidades.Find(Id);
                _db.Remove(tipVehiculos);
                _db.SaveChanges();
        }

        public void Edit(ComunidadRequest model)
        {
                Comunidades comunidad = _db.Comunidades.Find(model.Id);
                comunidad.Nombre = model.Nombre;
                comunidad.Descripcion = model.Descripcion;
                comunidad.Estado = model.Estado;
                comunidad.Fecha = DateTime.Now.Date;
                _db.Entry(comunidad).State = EntityState.Modified;
                _db.SaveChanges();
        }

        public void Get()
        {

        }
    }
}
