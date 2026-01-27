using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.MenuCategories.Commands.DeleteMenuCategory
{
    public record DeleteMenuCategoryCommand(Guid CategoryId):IRequest<ErrorOr<Deleted>>;
}
