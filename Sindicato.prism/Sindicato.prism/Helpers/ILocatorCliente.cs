using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sindicato.prism.Helpers
{
    public interface ILocatorCliente
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
        Task GeoclienteAsync();
    }
}
