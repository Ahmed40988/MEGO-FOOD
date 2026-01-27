using MediatR;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategoryById;

public record GetBaseCategoryByIdQuery(Guid Id) : IRequest<ErrorOr<BaseCategory>>;
