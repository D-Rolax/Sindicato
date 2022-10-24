using Plugin.Geolocator;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Models.Response;

namespace Sindicato.common.Services
{
    public class GeolocationService : IGeolocationService
    {
        public double Latitude { get ; set; }
        public double Longitude { get ; set; }

        public async Task GeolocationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var location = await locator.GetPositionAsync();
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
