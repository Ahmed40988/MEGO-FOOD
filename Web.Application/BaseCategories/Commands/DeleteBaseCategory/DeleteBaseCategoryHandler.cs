namespace Web.Application.BaseCategories.Commands.DeleteBaseCategory;

public class DeleteBaseCategoryHandler(IBaseCategoryRepository baseCategoryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteBaseCategoryCommand, ErrorOr<Deleted>>
{
    private readonly IBaseCategoryRepository _baseCategoryRepository = baseCategoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteBaseCategoryCommand command, CancellationToken cancellationToken)
    {
        var entity = await _baseCategoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
            return Error.NotFound("BaseCategory.NotFound", "BaseCategory not found");

        entity.Delete(command.UserId);//TODO add admin id
        await _baseCategoryRepository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.CommitChangesAsync();
        return Result.Deleted;
    }
}
