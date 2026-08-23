
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Web.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
      List<IFormFile>? ImagesURL,
    decimal Price,
    Guid MenuCategoryId,
    string AdminId
) : IRequest<ErrorOr<Guid>>;
