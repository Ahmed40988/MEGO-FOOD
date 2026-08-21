using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Users;

namespace Web.Domain.Addresses
{
    public class UserAddress: BaseAddress
    {
     
        public string UserId { get; private set; }
        public AppUser User { get; private set; } = null!;
        public UserAddress(string userId, double latitude, double longitude, string? address)
         : base(latitude, longitude, address)
        {
            UserId = userId;
        }



    }
}
