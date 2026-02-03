using Web.Application.BaseCategories.BaseCategoryDTO;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategoryById;

public class GetBaseCategoryByIdHandler
    : IRequestHandler<GetBaseCategoryByIdQuery, ErrorOr<BaseCategoryResponse>>
{
    private readonly IBaseCategoryRepository _repository;

    public GetBaseCategoryByIdHandler(IBaseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<BaseCategoryResponse>> Handle(GetBaseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
            return Error.NotFound(description: "category by this Id is not found !");

        var response=new BaseCategoryResponse(category.Id,category.Name,category.Description);
        return response;

    }
}
