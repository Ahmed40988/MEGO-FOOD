using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Addresses;

namespace Web.Application.Common.Interfaces
{
    public interface IAdressesRepository
    {
         Task AddAddressAsync(UserAddress Entity ,CancellationToken cancellationToken);

        Task<UserAddress> UserAdressExist(string UserId, double Lat, double Lng, CancellationToken cancellationToken);
}
}
