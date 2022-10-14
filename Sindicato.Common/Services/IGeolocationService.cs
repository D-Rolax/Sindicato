using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sindicato.common.Services
{
    public interface IGeolocationService
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
        Task GeolocationAsync();
    }
}
