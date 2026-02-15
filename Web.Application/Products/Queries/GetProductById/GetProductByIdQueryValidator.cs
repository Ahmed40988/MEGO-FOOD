
using FluentValidation;

namespace Web.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x=>x.ProductId).NotEmpty();
    }
}
