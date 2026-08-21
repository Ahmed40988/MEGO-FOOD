using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Restaurants;

namespace Web.Domain.Addresses
{
    public class RestaurantAdress:BaseAddress
    {

        public Guid RestaurantId { get; private set; }
        public Restaurant Restaurant { get; private set; } = null!;
        public RestaurantAdress(double latitude, double longitude, string? address, Guid restaurantId)
         : base(latitude, longitude, address)
        {
            RestaurantId = restaurantId;
        }
    }
}
