using ErrorOr;
using MediatR;
using Web.Application.BaseCategories;
using Web.Application.Common.Interfaces;
using Web.Domain.BaseCategories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Application.BaseCategories.Commands.CreateBaseCategory;

public class CreateBaseCategoryHandler(IUnitOfWork unitOfWork,IBaseCategoryRepository baseCategoryRepository,IUserRepository userRepository) : IRequestHandler<CreateBaseCategoryCommand, ErrorOr<Guid>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBaseCategoryRepository _baseCategoryRepository = baseCategoryRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<Guid>> Handle(CreateBaseCategoryCommand command, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(command.UserId, cancellationToken);

        if (!userExists)
        {
            return Error.Validation(
                code: "User.NotFound",
                description: "User does not exist");
        }
        var entity = new BaseCategory(command.Name, command.Description, command.UserId);


        await _baseCategoryRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.CommitChangesAsync();
        return entity.Id;
    }
}
