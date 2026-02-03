namespace Web.Application.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid ProductId, Guid MenuCategoryId) : IRequest<ErrorOr<Product>>;
}
