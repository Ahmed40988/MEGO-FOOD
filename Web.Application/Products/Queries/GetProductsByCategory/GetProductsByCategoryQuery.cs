
using ErrorOr;
using MediatR;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<ErrorOr<List<ProductResponse>>>;
