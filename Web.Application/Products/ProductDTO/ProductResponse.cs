using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.ProductDTO
{
    public record ProductResponse(Guid Id, string Name,string Description ,string ImageUrl, decimal Price,decimal Rating);
}
