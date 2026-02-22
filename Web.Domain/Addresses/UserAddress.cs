using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Users;


namespace Web.Domain.Addresses
{
    public class UserAddress
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public AppUser User { get; private set; } = null!;
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? Address { get; private set; }

        private UserAddress() { }

        public UserAddress(string userId, double lat, double lng, string? address)
        {
            UserId = userId;
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
