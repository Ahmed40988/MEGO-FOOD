
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Web.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    IFormFile? Image,
    decimal Price,
    string UserId
) : IRequest<ErrorOr<Guid>>;
