namespace Web.Application.Products.Queries.listProductQuery
{
    public record listProductQuery(Guid MenuCategoryId) : IRequest<ErrorOr<List<Product>>>;
}
