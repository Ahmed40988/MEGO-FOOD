using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid productId):IRequest<ErrorOr<Deleted>>;
}
