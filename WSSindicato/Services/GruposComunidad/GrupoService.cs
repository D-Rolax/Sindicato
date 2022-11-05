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
    public class GrupoService : IGrupoService
    {
        private readonly SindicatoContext db;

        public GrupoService(SindicatoContext db)
        {
            this.db = db;
        }
        public void Add(GrupoRequest model)
        {
            var grupo = new Grupos();
            grupo.Nombre = model.NombreGrupo;
            grupo.Descripcion = model.Descripcion;
            grupo.Estado = "Activo";
            grupo.Fecha = DateTime.Now.Date;
            db.Grupos.Add(grupo);
            db.SaveChanges();

        }

        public void delete(int Id)
        {
            Grupos grupos = db.Grupos.Find(Id);
            db.Remove(grupos);
            db.SaveChanges();
        }

        public void Edit(GrupoRequest model)
        {
            Grupos grupo = db.Grupos.Find(model.IdGrupo);
            grupo.Nombre = model.NombreGrupo;
            grupo.Descripcion = model.Descripcion;
            grupo.Estado = model.Estado;
            grupo.Fecha = DateTime.Now.Date;
            db.Entry(grupo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Get()
        {

        }
    }
}
