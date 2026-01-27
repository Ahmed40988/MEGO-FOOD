using MediatR;
using Web.Application.BaseCategories;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public class GetBaseCategoriesHandler
    : IRequestHandler<GetBaseCategoriesQuery, ErrorOr<List<BaseCategory>>>
{
    private readonly IBaseCategoryRepository _repository;

    public GetBaseCategoriesHandler(IBaseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<List<BaseCategory>>> Handle(GetBaseCategoriesQuery request, CancellationToken cancellationToken)
       { 
        var listcategories= await _repository.GetAllAsync(cancellationToken);
                     return listcategories is null ?
                 Error.NotFound(description: "BaseCategory is  Empty !")
                 :listcategories.ToList();

        }
}
