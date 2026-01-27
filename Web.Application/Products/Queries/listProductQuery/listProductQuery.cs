using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.Queries.listProductQuery
{
    public record listProductQuery(Guid MenuCategoryId):IRequest<ErrorOr<List<Product>>>;
}
