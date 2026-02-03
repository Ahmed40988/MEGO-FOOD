namespace Web.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : IRequest<ErrorOr<Deleted>>;
}
