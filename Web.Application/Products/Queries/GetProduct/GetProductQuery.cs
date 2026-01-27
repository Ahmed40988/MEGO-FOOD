using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid ProductId,Guid MenuCategoryId):IRequest<ErrorOr<Product>>;
}
