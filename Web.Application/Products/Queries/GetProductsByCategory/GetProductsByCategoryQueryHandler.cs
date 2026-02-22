
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Pagination;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductsByCategory;
public class GetProductsByCategoryQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<PaginatedList<ProductResponse>>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ErrorOr<PaginatedList<ProductResponse>>> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByCategoryAsync(query.Filters,query.CategoryId, cancellationToken);

        if(products == null)
        {
            return Error.NotFound("Products.NotFound","Products not found for the specified category.");
        }
        return products;
    }
}

