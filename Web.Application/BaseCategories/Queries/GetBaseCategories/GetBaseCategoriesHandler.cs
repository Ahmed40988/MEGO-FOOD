using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Application.Common;
using Web.Application.Common.Pagination;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public class GetBaseCategoriesHandler
    : IRequestHandler<GetBaseCategoriesQuery,ErrorOr<PaginatedList<BaseCategoryResponse>>>
{
    private readonly IBaseCategoryRepository _repository;

    public GetBaseCategoriesHandler(IBaseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<PaginatedList<BaseCategoryResponse>>> Handle(GetBaseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var listcategories = await _repository.GetAllAsync(request.Filters,cancellationToken);
        if (listcategories is null)
            return Error.NotFound(description: "BaseCategory is  Empty !");

        return listcategories;

    }
}
