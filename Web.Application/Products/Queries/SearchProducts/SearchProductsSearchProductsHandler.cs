using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.SearchProducts
{
    public class SearchProductsSearchProductsHandler(IProductRepository productRepository) : IRequestHandler<SearchProductsQuery, ErrorOr<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ErrorOr<List<ProductResponse>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
           return await _productRepository.SearchAsync(
           request.Keyword,
           cancellationToken);        
        }
    }
}
