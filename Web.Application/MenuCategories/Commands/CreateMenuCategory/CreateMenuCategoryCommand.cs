using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.MenuCategories;
using Web.Domain.Restaurants;

namespace Web.Application.MenuCategories.Commands.CreateMenuCategory
{
    public record CreateMenuCategoryCommand(
          string name,
          string description,
          Guid restaurantcategoryid) : IRequest<ErrorOr<MenuCategory>>;
}
