using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Models.Request
{
    public class VehiculosRequest
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
        //[Required]
        //[MinLength(1, ErrorMessage ="Deben existir Afiliados")]
        public List<Afiliado> Afiliados { get; set; }
        public VehiculosRequest()
        {
            this.Afiliados = new List<Afiliado>();
        }
    }
    public class Afiliado
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Ci { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
