
using ErrorOr;
using MediatR;

namespace Web.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId,string UserId) : IRequest<ErrorOr<Success>>;
