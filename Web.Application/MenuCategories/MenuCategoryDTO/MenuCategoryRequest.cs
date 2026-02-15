using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.MenuCategories.MenuCategoryDTO
{
    public record MenuCategoryRequest(string Name,
          string Description,
          Guid Restaurantid);
}
