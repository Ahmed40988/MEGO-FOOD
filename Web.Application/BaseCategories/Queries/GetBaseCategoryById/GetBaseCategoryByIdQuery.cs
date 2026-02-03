using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategoryById;

public record GetBaseCategoryByIdQuery(Guid Id) : IRequest<ErrorOr<BaseCategoryResponse>>;
