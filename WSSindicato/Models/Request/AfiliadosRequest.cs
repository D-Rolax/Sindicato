using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Models.Request
{
    public class AfiliadosRequest
    {
        public int IdTipoVehiculo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Ci { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }
        public AfiliadosRequest()
        {
            this.Vehiculos = new List<Vehiculo>();
        }
    }
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
    }
}
