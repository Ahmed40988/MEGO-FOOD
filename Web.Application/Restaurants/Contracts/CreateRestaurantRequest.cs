using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.RestaurantCategories.Contracts
{
    public record CreateRestaurantRequest(
          string name,
          string description,
          string userId);
}
