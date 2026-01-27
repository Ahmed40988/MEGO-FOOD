using Azure.Core;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string name,
        string description,
        string imageUrl,
        decimal price,
        Guid menuCategoryId):IRequest<ErrorOr<Product>>;
}
