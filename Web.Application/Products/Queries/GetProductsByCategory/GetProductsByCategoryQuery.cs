
using ErrorOr;
using MediatR;
using Web.Application.Common;
using Web.Application.Common.Pagination;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(RequestFilters Filters,Guid CategoryId) : IRequest<ErrorOr<PaginatedList<ProductResponse>>>;
