using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.BaseCategories.BaseCategoryDTO
{
    public record BaseCategoryResponse(Guid Id,string name,string Description);
}
