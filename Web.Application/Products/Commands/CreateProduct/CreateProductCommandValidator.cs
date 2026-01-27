using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.name)
                .MinimumLength(3)
                .MaximumLength(15);

            RuleFor(x => x.description)
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.price)
                .GreaterThan(0);       
        }

    }
}
