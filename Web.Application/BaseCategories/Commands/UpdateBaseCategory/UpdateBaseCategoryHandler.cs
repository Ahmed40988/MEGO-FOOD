using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Commands.UpdateBaseCategory;

public class UpdateBaseCategoryHandler(IUnitOfWork unitOfWork, IBaseCategoryRepository baseCategoryRepository, IUserRepository userRepository) : IRequestHandler<UpdateBaseCategoryCommand, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBaseCategoryRepository _baseCategoryRepository = baseCategoryRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<Guid>> Handle(UpdateBaseCategoryCommand command, CancellationToken cancellationToken)
    {
        var adminExists = await _userRepository.ExistsAsync(command.AdminId, cancellationToken);


        if (!adminExists)
        {
            return Error.Validation(
                code: "admin.NotFound",
                description: "admin does not exist");
        }
        var entity = await _baseCategoryRepository.GetByIdAsync(command.Id);
        if (entity is null)
            return Error.NotFound("Base Category.NotFound", "Base Category With this Id is not Found");

        var exist = await _baseCategoryRepository.GetByNameAsync(command.Name, cancellationToken);

        if (exist is not null && exist.Id != command.Id)
        {
            return Error.Conflict(
                "BaseCategory.Duplicated",
                "Base Category with the same name already exists"
            );
        }


        entity.Update(command.AdminId, command.Name, command.Description);
        await _unitOfWork.CommitChangesAsync();
        return entity.Id;
    }
}
