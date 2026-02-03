using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public record GetBaseCategoriesQuery() : IRequest<ErrorOr<List<BaseCategoryResponse>>>;
