using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Users;


namespace Web.Domain.Addresses
{
    public abstract class BaseAddress
    {
        public Guid Id { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? Address { get; private set; }

        private BaseAddress() { }

        public BaseAddress( double lat, double lng, string? address)
        {
            Latitude = lat;
            Longitude = lng;
            Address = address;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
        }
    }
}
