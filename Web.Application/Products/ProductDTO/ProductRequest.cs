
using Microsoft.AspNetCore.Http;

namespace Web.Application.Products.ProductDTO;

public record ProductRequest(
    string Name,
    string Description,
    IFormFile? Image,
    decimal Price,
    Guid MenuCategoryId
);
