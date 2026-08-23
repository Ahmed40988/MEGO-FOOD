
using Microsoft.AspNetCore.Http;

namespace Web.Application.Products.ProductDTO;

public record ProductRequest(
    string Name,
    string Description,
    List<IFormFile>? Images,
    decimal Price,
    Guid MenuCategoryId
);
