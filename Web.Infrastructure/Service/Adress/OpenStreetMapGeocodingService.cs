using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Application.Common.Interfaces;


namespace Web.Infrastructure.Service.Adress
{
    public class OpenStreetMapGeocodingService
        : IReverseGeocodingService
    {
        private readonly HttpClient _http;

        public OpenStreetMapGeocodingService(HttpClient http)
        {
            _http = http;
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("MEGOFOOD_APP");
        }

        public async Task<string?> GetAddressAsync(double lat, double lng)
        {
            var url =
            $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lng}&format=json";

            var response = await _http.GetStringAsync(url);

            dynamic data = JsonConvert.DeserializeObject(response);

            return data.display_name;
        }
    }
}
