
using ErrorOr;
using MediatR;
using Web.Application.Products.ProductDTO;

namespace Web.Application.Products.Queries.GetProductById;
public class GetProductByIdQueryHandler(IProductRepository productRepository)
: IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.ProductId);

        if(product is null)
            return Error.NotFound("Product.NotFound","Product not found");

        return new ProductResponse(product.Id,product.Name,product.Description,product.ImagesURL,product.Price,product.Rating);
    }
}
