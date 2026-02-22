using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Common.Interfaces
{
    public interface IReverseGeocodingService
    {
        Task<string?> GetAddressAsync(double lat, double lng);
    }
}
