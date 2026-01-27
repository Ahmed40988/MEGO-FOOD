using ErrorOr;
using MediatR;

namespace Web.Application.BaseCategories.Commands.DeleteBaseCategory;

public record DeleteBaseCategoryCommand(Guid Id, string UserId)
    : IRequest<ErrorOr<Deleted>>;
