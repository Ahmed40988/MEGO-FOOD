using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.listMenuCategory
{
    public record listMenuCategoriesQuery(Guid RestaurantCatgoryId):IRequest<ErrorOr<List<MenuCategory>>>;
}
