using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.MenuCategories;

namespace Web.Application.MenuCategories.Queries.GetMenuCategory
{
    public record GetMenuCategoryQuery(Guid RestaurantCategoryId,Guid menuCategoryId):IRequest<ErrorOr<MenuCategory>>;
}
