using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infrastructure.Seeding
{
    public class SeedSettings
    {
        public int RestaurantsPerCategory { get; set; } = 10;
        public int ProductsPerRestaurant { get; set; } = 50;
    }
}
