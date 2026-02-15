
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductsByCategory;
public class GetProductsByCategoryQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsByCategoryQuery, List<ProductResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<List<ProductResponse>> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByCategoryAsync(query.CategoryId, cancellationToken);

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

