namespace Web.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string name,
        string description,
        string imageUrl,
        decimal price,
        Guid menuCategoryId) : IRequest<ErrorOr<Product>>;
}
