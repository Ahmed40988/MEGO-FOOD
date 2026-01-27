using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.MenuCategories;

namespace Web.Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(IMenuCategoryRepository menuCategoryRepository,IProductRepository productRepository) : IRequestHandler<GetProductQuery, ErrorOr<Product>>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {

            if (!await _menuCategoryRepository.ExistsAsync(query.MenuCategoryId, cancellationToken))
            {
                return Error.NotFound("Menu Category  not found");
            }

            if (await _productRepository.GetByIdAsync(query.ProductId, cancellationToken) is not Product product)
            {
                return Error.NotFound(description: "product not found");
            }

            return product;
        }
    }
}
