using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;

namespace WSSindicato.Services.HorarioViajes
{
    public class RutasService : IRutasService
    {
        private readonly string _connectionString;
        private readonly SindicatoContext _db;

        public RutasService(IConfiguration configuration,SindicatoContext db)
        {
            _connectionString = configuration.GetConnectionString("Cn");
            _db = db;
        }
        public void AddRuta(RutasResponse model)
        {
            //Comunidades comunidad = _db.Comunidades.Find(model);
            //Grupos grupo = _db.Grupos.Find(model);
            foreach (var item in model.Rutas)
            {
                var rutas = new Rutas();
                rutas.ComunidadId = model.IdComunidad;
                rutas.GrupoId = model.IdGrupo;
                rutas.Latitud = item.Latitud;
                rutas.Longitud = item.Longitud;
                _db.Rutas.Add(rutas);
                _db.SaveChanges();
            }
        }

        public void cambioEstado()
        {
            throw new NotImplementedException();
        }

        public async Task delete(RutasResponse model)
        {
            using (SqlConnection db=new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("sp_delete_rutas",db))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdComunidad", model.IdComunidad));
                    cmd.Parameters.Add(new SqlParameter("IdGrupo", model.IdGrupo));
                    await db.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<List<RutasResponse>> getRutas()
        {
            using (SqlConnection db= new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_MostrarRutas",db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var response = new List<RutasResponse>();
                    await db.OpenAsync();
                    using (var reader= await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapTovalue(reader));
                        }
                    }
                    return response;
                }
            }
        }
        private RutasResponse MapTovalue(SqlDataReader reader)
        {
            return new RutasResponse()
            {
                IdComunidad = (int)reader["IdComunidad"],
                NombreComunidad = reader["NombreComunidad"].ToString(),
                IdGrupo = (int)reader["IdGrupo"],
                NombreGrupo = reader["NombreGrupo"].ToString(),
                Estado = reader["Estado"].ToString()
            };
        }
    }
}
