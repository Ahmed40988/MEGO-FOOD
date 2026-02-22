using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Common;
using Web.Application.Common.Pagination;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public record GetBaseCategoriesQuery(RequestFilters Filters)
    : IRequest<ErrorOr<PaginatedList<BaseCategoryResponse>>>;
