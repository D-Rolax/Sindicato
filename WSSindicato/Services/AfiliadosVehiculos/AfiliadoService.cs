using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;

namespace WSSindicato.Services
{
    public class AfiliadoService : IAfiliadoService
    {
        private readonly SindicatoContext db;

        public AfiliadoService(SindicatoContext db)
        {
            this.db = db;
        }
        public void delete(int Id)
        {
            Afiliados afiliados = db.Afiliados.Find(Id);
            db.Remove(afiliados);
            db.SaveChanges();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
