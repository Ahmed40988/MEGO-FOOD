using MediatR;
using Web.Application.BaseCategories;
using Web.Domain.BaseCategories;
using Web.Domain.Restaurants;

namespace Web.Application.BaseCategories.Queries.GetBaseCategoryById;

public class GetBaseCategoryByIdHandler
    : IRequestHandler<GetBaseCategoryByIdQuery,ErrorOr<BaseCategory>>
{
    private readonly IBaseCategoryRepository _repository;

    public GetBaseCategoryByIdHandler(IBaseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<BaseCategory>> Handle(GetBaseCategoryByIdQuery request, CancellationToken cancellationToken)
    { 
       var category= await _repository.GetByIdAsync(request.Id, cancellationToken);
        return category is null ?
              Error.NotFound(description: "category by this Id is not found !")
              : category;

    }
}
