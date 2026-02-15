
using ErrorOr;
using MediatR;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ErrorOr<ProductResponse>>;
