using ErrorOr;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.SearchProducts
{
    public record SearchProductsQuery(string Keyword):IRequest<ErrorOr<List<ProductResponse>>>;
}
