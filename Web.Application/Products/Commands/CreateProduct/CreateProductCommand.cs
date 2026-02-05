using MediatR;

namespace Web.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    Guid MenuCategoryId,
    Guid UserId
) : IRequest<ErrorOr<Guid>>;
