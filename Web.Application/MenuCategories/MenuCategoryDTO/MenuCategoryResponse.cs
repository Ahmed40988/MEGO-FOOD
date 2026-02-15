using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.MenuCategories.MenuCategoryDTO
{
    public record MenuCategoryResponse(Guid MenuCategoryId, string name,
          string description,
          Guid restaurantid);
}
