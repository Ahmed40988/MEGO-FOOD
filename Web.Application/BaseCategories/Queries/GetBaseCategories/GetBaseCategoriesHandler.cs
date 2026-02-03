using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public class GetBaseCategoriesHandler
    : IRequestHandler<GetBaseCategoriesQuery, ErrorOr<List<BaseCategoryResponse>>>
{
    private readonly IBaseCategoryRepository _repository;

    public GetBaseCategoriesHandler(IBaseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<List<BaseCategoryResponse>>> Handle(GetBaseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var listcategories = await _repository.GetAllAsync(cancellationToken);
        if (listcategories is null)
            return Error.NotFound(description: "BaseCategory is  Empty !");

        var list =new List<BaseCategoryResponse>();
        BaseCategoryResponse item = null;
        foreach (var category in listcategories)
        {
             item = new BaseCategoryResponse(category.Id, category.Name, category.Description);

            list.Add(item);
        }
        return  list;

    }
}
