
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductsByCategory;
public class GetProductsByCategoryQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsByCategoryQuery, ErrorOr<List<ProductResponse>>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ErrorOr<List<ProductResponse>>> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByCategoryAsync(query.CategoryId, cancellationToken);

        if(products == null || !products.Any())
        {
            return Error.NotFound("Products.NotFound","Products not found for the specified category.");
        }
        return products
            .Select(x => new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.ImageUrl,
                x.Price))
            .ToList();
    }
}

